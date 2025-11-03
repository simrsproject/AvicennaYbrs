/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/18/2018 8:55:10 AM
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
	abstract public class esParamedicFeePaymentHdCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeePaymentHdCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeePaymentHdCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentHdQuery query)
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
			this.InitQuery(query as esParamedicFeePaymentHdQuery);
		}
		#endregion
			
		virtual public ParamedicFeePaymentHd DetachEntity(ParamedicFeePaymentHd entity)
		{
			return base.DetachEntity(entity) as ParamedicFeePaymentHd;
		}
		
		virtual public ParamedicFeePaymentHd AttachEntity(ParamedicFeePaymentHd entity)
		{
			return base.AttachEntity(entity) as ParamedicFeePaymentHd;
		}
		
		virtual public void Combine(ParamedicFeePaymentHdCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeePaymentHd this[int index]
		{
			get
			{
				return base[index] as ParamedicFeePaymentHd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeePaymentHd);
		}
	}

	[Serializable]
	abstract public class esParamedicFeePaymentHd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeePaymentHdQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeePaymentHd()
		{
		}
	
		public esParamedicFeePaymentHd(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paymentNo)
		{
			esParamedicFeePaymentHdQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo==paymentNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paymentNo)
		{
			esParameters parms = new esParameters();
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "PaymentMethodID": this.str.PaymentMethodID = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
						case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PaymentDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						case "PaymentAmount":
						
							if (value == null || value is System.Decimal)
								this.PaymentAmount = (System.Decimal?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
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
		/// Maps to ParamedicFeePaymentHd.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.PaymentMethodID
		/// </summary>
		virtual public System.String PaymentMethodID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentMethodID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentMethodID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.BankAccountNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.PaymentAmount
		/// </summary>
		virtual public System.Decimal? PaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeePaymentHdMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeePaymentHdMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeePaymentHdMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeePaymentHdMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentHd.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentGroupNo, value);
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
			public esStrings(esParamedicFeePaymentHd entity)
			{
				this.entity = entity;
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
			public System.String PaymentDate
			{
				get
				{
					System.DateTime? data = entity.PaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentDate = null;
					else entity.PaymentDate = Convert.ToDateTime(value);
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
			public System.String PaymentMethodID
			{
				get
				{
					System.String data = entity.PaymentMethodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentMethodID = null;
					else entity.PaymentMethodID = Convert.ToString(value);
				}
			}
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String BankAccountNo
			{
				get
				{
					System.String data = entity.BankAccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountNo = null;
					else entity.BankAccountNo = Convert.ToString(value);
				}
			}
			public System.String PaymentAmount
			{
				get
				{
					System.Decimal? data = entity.PaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentAmount = null;
					else entity.PaymentAmount = Convert.ToDecimal(value);
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
			public System.String PaymentGroupNo
			{
				get
				{
					System.String data = entity.PaymentGroupNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentGroupNo = null;
					else entity.PaymentGroupNo = Convert.ToString(value);
				}
			}
			private esParamedicFeePaymentHd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentHdQuery query)
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
				throw new Exception("esParamedicFeePaymentHd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeePaymentHd : esParamedicFeePaymentHd
	{	
	}

	[Serializable]
	abstract public class esParamedicFeePaymentHdQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentHdMetadata.Meta();
			}
		}	
			
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentMethodID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.PaymentMethodID, esSystemType.String);
			}
		} 
			
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
			
		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentHdMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeePaymentHdCollection")]
	public partial class ParamedicFeePaymentHdCollection : esParamedicFeePaymentHdCollection, IEnumerable< ParamedicFeePaymentHd>
	{
		public ParamedicFeePaymentHdCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeePaymentHd>(ParamedicFeePaymentHdCollection coll)
		{
			List< ParamedicFeePaymentHd> list = new List< ParamedicFeePaymentHd>();
			
			foreach (ParamedicFeePaymentHd emp in coll)
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
				return  ParamedicFeePaymentHdMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeePaymentHd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeePaymentHd();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeePaymentHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentHdQuery();
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
		public bool Load(ParamedicFeePaymentHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeePaymentHd AddNew()
		{
			ParamedicFeePaymentHd entity = base.AddNewEntity() as ParamedicFeePaymentHd;
			
			return entity;		
		}
		public ParamedicFeePaymentHd FindByPrimaryKey(String paymentNo)
		{
			return base.FindByPrimaryKey(paymentNo) as ParamedicFeePaymentHd;
		}

		#region IEnumerable< ParamedicFeePaymentHd> Members

		IEnumerator< ParamedicFeePaymentHd> IEnumerable< ParamedicFeePaymentHd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeePaymentHd;
			}
		}

		#endregion
		
		private ParamedicFeePaymentHdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeePaymentHd' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeePaymentHd ({PaymentNo})")]
	[Serializable]
	public partial class ParamedicFeePaymentHd : esParamedicFeePaymentHd
	{
		public ParamedicFeePaymentHd()
		{
		}	
	
		public ParamedicFeePaymentHd(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentHdMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeePaymentHdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeePaymentHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentHdQuery();
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
		public bool Load(ParamedicFeePaymentHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeePaymentHdQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeePaymentHdQuery : esParamedicFeePaymentHdQuery
	{
		public ParamedicFeePaymentHdQuery()
		{

		}		
		
		public ParamedicFeePaymentHdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeePaymentHdQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeePaymentHdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeePaymentHdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.PaymentDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentMethodID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.PaymentMethodID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.BankID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.BankAccountNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.BankAccountNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.PaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentHdMetadata.ColumnNames.PaymentGroupNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentHdMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeePaymentHdMetadata Meta()
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
			public const string PaymentNo = "PaymentNo";
			public const string PaymentDate = "PaymentDate";
			public const string ParamedicID = "ParamedicID";
			public const string PaymentMethodID = "PaymentMethodID";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string PaymentAmount = "PaymentAmount";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PaymentGroupNo = "PaymentGroupNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PaymentNo = "PaymentNo";
			public const string PaymentDate = "PaymentDate";
			public const string ParamedicID = "ParamedicID";
			public const string PaymentMethodID = "PaymentMethodID";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string PaymentAmount = "PaymentAmount";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PaymentGroupNo = "PaymentGroupNo";
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
			lock (typeof(ParamedicFeePaymentHdMetadata))
			{
				if(ParamedicFeePaymentHdMetadata.mapDelegates == null)
				{
					ParamedicFeePaymentHdMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeePaymentHdMetadata.meta == null)
				{
					ParamedicFeePaymentHdMetadata.meta = new ParamedicFeePaymentHdMetadata();
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
				
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentMethodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeePaymentHd";
				meta.Destination = "ParamedicFeePaymentHd";
				meta.spInsert = "proc_ParamedicFeePaymentHdInsert";				
				meta.spUpdate = "proc_ParamedicFeePaymentHdUpdate";		
				meta.spDelete = "proc_ParamedicFeePaymentHdDelete";
				meta.spLoadAll = "proc_ParamedicFeePaymentHdLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeePaymentHdLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeePaymentHdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
