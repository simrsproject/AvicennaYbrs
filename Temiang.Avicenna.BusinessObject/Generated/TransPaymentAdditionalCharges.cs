/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/24/2020 12:07:39 AM
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
	abstract public class esTransPaymentAdditionalChargesCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentAdditionalChargesCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TransPaymentAdditionalChargesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTransPaymentAdditionalChargesQuery query)
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
			this.InitQuery(query as esTransPaymentAdditionalChargesQuery);
		}
		#endregion
			
		virtual public TransPaymentAdditionalCharges DetachEntity(TransPaymentAdditionalCharges entity)
		{
			return base.DetachEntity(entity) as TransPaymentAdditionalCharges;
		}
		
		virtual public TransPaymentAdditionalCharges AttachEntity(TransPaymentAdditionalCharges entity)
		{
			return base.AttachEntity(entity) as TransPaymentAdditionalCharges;
		}
		
		virtual public void Combine(TransPaymentAdditionalChargesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPaymentAdditionalCharges this[int index]
		{
			get
			{
				return base[index] as TransPaymentAdditionalCharges;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentAdditionalCharges);
		}
	}

	[Serializable]
	abstract public class esTransPaymentAdditionalCharges : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentAdditionalChargesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTransPaymentAdditionalCharges()
		{
		}
	
		public esTransPaymentAdditionalCharges(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRCafeAdditionalCharges, String paymentNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRCafeAdditionalCharges, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(sRCafeAdditionalCharges, paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRCafeAdditionalCharges, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRCafeAdditionalCharges, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(sRCafeAdditionalCharges, paymentNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String sRCafeAdditionalCharges, String paymentNo)
		{
			esTransPaymentAdditionalChargesQuery query = this.GetDynamicQuery();
			query.Where(query.SRCafeAdditionalCharges==sRCafeAdditionalCharges, query.PaymentNo==paymentNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String sRCafeAdditionalCharges, String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("SRCafeAdditionalCharges",sRCafeAdditionalCharges);
			parms.Add("PaymentNo",paymentNo);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "SRCafeAdditionalCharges": this.str.SRCafeAdditionalCharges = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ChargeAmount": this.str.ChargeAmount = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ChargeAmount":
						
							if (value == null || value is System.Decimal)
								this.ChargeAmount = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.SRCafeAdditionalCharges
		/// </summary>
		virtual public System.String SRCafeAdditionalCharges
		{
			get
			{
				return base.GetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.SRCafeAdditionalCharges);
			}
			
			set
			{
				base.SetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.SRCafeAdditionalCharges, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.ChargeAmount
		/// </summary>
		virtual public System.Decimal? ChargeAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentAdditionalChargesMetadata.ColumnNames.ChargeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPaymentAdditionalChargesMetadata.ColumnNames.ChargeAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentAdditionalChargesMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentAdditionalChargesMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentAdditionalCharges.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentAdditionalChargesMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransPaymentAdditionalChargesMetadata.ColumnNames.IsVoid, value);
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
		[BrowsableAttribute( false )]		
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
			public esStrings(esTransPaymentAdditionalCharges entity)
			{
				this.entity = entity;
			}
			public System.String SRCafeAdditionalCharges
			{
				get
				{
					System.String data = entity.SRCafeAdditionalCharges;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCafeAdditionalCharges = null;
					else entity.SRCafeAdditionalCharges = Convert.ToString(value);
				}
			}
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
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
			public System.String ChargeAmount
			{
				get
				{
					System.Decimal? data = entity.ChargeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChargeAmount = null;
					else entity.ChargeAmount = Convert.ToDecimal(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			private esTransPaymentAdditionalCharges entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentAdditionalChargesQuery query)
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
				throw new Exception("esTransPaymentAdditionalCharges can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentAdditionalCharges : esTransPaymentAdditionalCharges
	{	
	}

	[Serializable]
	abstract public class esTransPaymentAdditionalChargesQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentAdditionalChargesMetadata.Meta();
			}
		}	
			
		public esQueryItem SRCafeAdditionalCharges
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.SRCafeAdditionalCharges, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ChargeAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.ChargeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPaymentAdditionalChargesMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentAdditionalChargesCollection")]
	public partial class TransPaymentAdditionalChargesCollection : esTransPaymentAdditionalChargesCollection, IEnumerable< TransPaymentAdditionalCharges>
	{
		public TransPaymentAdditionalChargesCollection()
		{

		}	
		
		public static implicit operator List< TransPaymentAdditionalCharges>(TransPaymentAdditionalChargesCollection coll)
		{
			List< TransPaymentAdditionalCharges> list = new List< TransPaymentAdditionalCharges>();
			
			foreach (TransPaymentAdditionalCharges emp in coll)
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
				return  TransPaymentAdditionalChargesMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentAdditionalChargesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentAdditionalCharges(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentAdditionalCharges();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TransPaymentAdditionalChargesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentAdditionalChargesQuery();
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
		public bool Load(TransPaymentAdditionalChargesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentAdditionalCharges AddNew()
		{
			TransPaymentAdditionalCharges entity = base.AddNewEntity() as TransPaymentAdditionalCharges;
			
			return entity;		
		}
		public TransPaymentAdditionalCharges FindByPrimaryKey(String sRCafeAdditionalCharges, String paymentNo)
		{
			return base.FindByPrimaryKey(sRCafeAdditionalCharges, paymentNo) as TransPaymentAdditionalCharges;
		}

		#region IEnumerable< TransPaymentAdditionalCharges> Members

		IEnumerator< TransPaymentAdditionalCharges> IEnumerable< TransPaymentAdditionalCharges>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentAdditionalCharges;
			}
		}

		#endregion
		
		private TransPaymentAdditionalChargesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentAdditionalCharges' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentAdditionalCharges ({SRCafeAdditionalCharges, PaymentNo})")]
	[Serializable]
	public partial class TransPaymentAdditionalCharges : esTransPaymentAdditionalCharges
	{
		public TransPaymentAdditionalCharges()
		{
		}	
	
		public TransPaymentAdditionalCharges(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentAdditionalChargesMetadata.Meta();
			}
		}	
	
		override protected esTransPaymentAdditionalChargesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentAdditionalChargesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TransPaymentAdditionalChargesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentAdditionalChargesQuery();
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
		public bool Load(TransPaymentAdditionalChargesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TransPaymentAdditionalChargesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentAdditionalChargesQuery : esTransPaymentAdditionalChargesQuery
	{
		public TransPaymentAdditionalChargesQuery()
		{

		}		
		
		public TransPaymentAdditionalChargesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TransPaymentAdditionalChargesQuery";
        }
	}

	[Serializable]
	public partial class TransPaymentAdditionalChargesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentAdditionalChargesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.SRCafeAdditionalCharges, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.SRCafeAdditionalCharges;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.ChargeAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.ChargeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentAdditionalChargesMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentAdditionalChargesMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TransPaymentAdditionalChargesMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string SRCafeAdditionalCharges = "SRCafeAdditionalCharges";
			public const string PaymentNo = "PaymentNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string ChargeAmount = "ChargeAmount";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsVoid = "IsVoid";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SRCafeAdditionalCharges = "SRCafeAdditionalCharges";
			public const string PaymentNo = "PaymentNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string ChargeAmount = "ChargeAmount";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsVoid = "IsVoid";
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
			lock (typeof(TransPaymentAdditionalChargesMetadata))
			{
				if(TransPaymentAdditionalChargesMetadata.mapDelegates == null)
				{
					TransPaymentAdditionalChargesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPaymentAdditionalChargesMetadata.meta == null)
				{
					TransPaymentAdditionalChargesMetadata.meta = new TransPaymentAdditionalChargesMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("SRCafeAdditionalCharges", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChargeAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "TransPaymentAdditionalCharges";
				meta.Destination = "TransPaymentAdditionalCharges";
				meta.spInsert = "proc_TransPaymentAdditionalChargesInsert";				
				meta.spUpdate = "proc_TransPaymentAdditionalChargesUpdate";		
				meta.spDelete = "proc_TransPaymentAdditionalChargesDelete";
				meta.spLoadAll = "proc_TransPaymentAdditionalChargesLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentAdditionalChargesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentAdditionalChargesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
