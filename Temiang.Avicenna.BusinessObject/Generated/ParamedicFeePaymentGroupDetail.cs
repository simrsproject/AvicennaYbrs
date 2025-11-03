/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/1/2020 8:47:13 PM
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
	abstract public class esParamedicFeePaymentGroupDetailCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeePaymentGroupDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeePaymentGroupDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentGroupDetailQuery query)
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
			this.InitQuery(query as esParamedicFeePaymentGroupDetailQuery);
		}
		#endregion
			
		virtual public ParamedicFeePaymentGroupDetail DetachEntity(ParamedicFeePaymentGroupDetail entity)
		{
			return base.DetachEntity(entity) as ParamedicFeePaymentGroupDetail;
		}
		
		virtual public ParamedicFeePaymentGroupDetail AttachEntity(ParamedicFeePaymentGroupDetail entity)
		{
			return base.AttachEntity(entity) as ParamedicFeePaymentGroupDetail;
		}
		
		virtual public void Combine(ParamedicFeePaymentGroupDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeePaymentGroupDetail this[int index]
		{
			get
			{
				return base[index] as ParamedicFeePaymentGroupDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeePaymentGroupDetail);
		}
	}

	[Serializable]
	abstract public class esParamedicFeePaymentGroupDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeePaymentGroupDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeePaymentGroupDetail()
		{
		}
	
		public esParamedicFeePaymentGroupDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentGroupNo, String paramedicID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentGroupNo, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentGroupNo, paramedicID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentGroupNo, String paramedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentGroupNo, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentGroupNo, paramedicID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paymentGroupNo, String paramedicID)
		{
			esParamedicFeePaymentGroupDetailQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentGroupNo==paymentGroupNo, query.ParamedicID==paramedicID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paymentGroupNo, String paramedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentGroupNo",paymentGroupNo);
			parms.Add("ParamedicID",paramedicID);
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
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "AmountFee4Service": this.str.AmountFee4Service = (string)value; break;
						case "AmountAddDec": this.str.AmountAddDec = (string)value; break;
						case "AmountGuarantee": this.str.AmountGuarantee = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AmountFee4Service":
						
							if (value == null || value is System.Decimal)
								this.AmountFee4Service = (System.Decimal?)value;
							break;
						case "AmountAddDec":
						
							if (value == null || value is System.Decimal)
								this.AmountAddDec = (System.Decimal?)value;
							break;
						case "AmountGuarantee":
						
							if (value == null || value is System.Decimal)
								this.AmountGuarantee = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to ParamedicFeePaymentGroupDetail.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.AmountFee4Service
		/// </summary>
		virtual public System.Decimal? AmountFee4Service
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountFee4Service);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountFee4Service, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.AmountAddDec
		/// </summary>
		virtual public System.Decimal? AmountAddDec
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountAddDec);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountAddDec, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.AmountGuarantee
		/// </summary>
		virtual public System.Decimal? AmountGuarantee
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountGuarantee);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountGuarantee, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroupDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esParamedicFeePaymentGroupDetail entity)
			{
				this.entity = entity;
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
			public System.String AmountFee4Service
			{
				get
				{
					System.Decimal? data = entity.AmountFee4Service;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountFee4Service = null;
					else entity.AmountFee4Service = Convert.ToDecimal(value);
				}
			}
			public System.String AmountAddDec
			{
				get
				{
					System.Decimal? data = entity.AmountAddDec;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountAddDec = null;
					else entity.AmountAddDec = Convert.ToDecimal(value);
				}
			}
			public System.String AmountGuarantee
			{
				get
				{
					System.Decimal? data = entity.AmountGuarantee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountGuarantee = null;
					else entity.AmountGuarantee = Convert.ToDecimal(value);
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
			private esParamedicFeePaymentGroupDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentGroupDetailQuery query)
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
				throw new Exception("esParamedicFeePaymentGroupDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeePaymentGroupDetail : esParamedicFeePaymentGroupDetail
	{	
	}

	[Serializable]
	abstract public class esParamedicFeePaymentGroupDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentGroupDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem AmountFee4Service
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountFee4Service, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AmountAddDec
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountAddDec, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AmountGuarantee
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountGuarantee, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeePaymentGroupDetailCollection")]
	public partial class ParamedicFeePaymentGroupDetailCollection : esParamedicFeePaymentGroupDetailCollection, IEnumerable< ParamedicFeePaymentGroupDetail>
	{
		public ParamedicFeePaymentGroupDetailCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeePaymentGroupDetail>(ParamedicFeePaymentGroupDetailCollection coll)
		{
			List< ParamedicFeePaymentGroupDetail> list = new List< ParamedicFeePaymentGroupDetail>();
			
			foreach (ParamedicFeePaymentGroupDetail emp in coll)
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
				return  ParamedicFeePaymentGroupDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentGroupDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeePaymentGroupDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeePaymentGroupDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeePaymentGroupDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentGroupDetailQuery();
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
		public bool Load(ParamedicFeePaymentGroupDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeePaymentGroupDetail AddNew()
		{
			ParamedicFeePaymentGroupDetail entity = base.AddNewEntity() as ParamedicFeePaymentGroupDetail;
			
			return entity;		
		}
		public ParamedicFeePaymentGroupDetail FindByPrimaryKey(String paymentGroupNo, String paramedicID)
		{
			return base.FindByPrimaryKey(paymentGroupNo, paramedicID) as ParamedicFeePaymentGroupDetail;
		}

		#region IEnumerable< ParamedicFeePaymentGroupDetail> Members

		IEnumerator< ParamedicFeePaymentGroupDetail> IEnumerable< ParamedicFeePaymentGroupDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeePaymentGroupDetail;
			}
		}

		#endregion
		
		private ParamedicFeePaymentGroupDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeePaymentGroupDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeePaymentGroupDetail ({PaymentGroupNo, ParamedicID})")]
	[Serializable]
	public partial class ParamedicFeePaymentGroupDetail : esParamedicFeePaymentGroupDetail
	{
		public ParamedicFeePaymentGroupDetail()
		{
		}	
	
		public ParamedicFeePaymentGroupDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentGroupDetailMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeePaymentGroupDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentGroupDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeePaymentGroupDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentGroupDetailQuery();
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
		public bool Load(ParamedicFeePaymentGroupDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeePaymentGroupDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeePaymentGroupDetailQuery : esParamedicFeePaymentGroupDetailQuery
	{
		public ParamedicFeePaymentGroupDetailQuery()
		{

		}		
		
		public ParamedicFeePaymentGroupDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeePaymentGroupDetailQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeePaymentGroupDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeePaymentGroupDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.PaymentGroupNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.PaymentGroupNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountFee4Service, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.AmountFee4Service;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountAddDec, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.AmountAddDec;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.AmountGuarantee, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.AmountGuarantee;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupDetailMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeePaymentGroupDetailMetadata Meta()
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
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string ParamedicID = "ParamedicID";
			public const string AmountFee4Service = "AmountFee4Service";
			public const string AmountAddDec = "AmountAddDec";
			public const string AmountGuarantee = "AmountGuarantee";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string ParamedicID = "ParamedicID";
			public const string AmountFee4Service = "AmountFee4Service";
			public const string AmountAddDec = "AmountAddDec";
			public const string AmountGuarantee = "AmountGuarantee";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
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
			lock (typeof(ParamedicFeePaymentGroupDetailMetadata))
			{
				if(ParamedicFeePaymentGroupDetailMetadata.mapDelegates == null)
				{
					ParamedicFeePaymentGroupDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeePaymentGroupDetailMetadata.meta == null)
				{
					ParamedicFeePaymentGroupDetailMetadata.meta = new ParamedicFeePaymentGroupDetailMetadata();
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
				
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AmountFee4Service", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AmountAddDec", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AmountGuarantee", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ParamedicFeePaymentGroupDetail";
				meta.Destination = "ParamedicFeePaymentGroupDetail";
				meta.spInsert = "proc_ParamedicFeePaymentGroupDetailInsert";				
				meta.spUpdate = "proc_ParamedicFeePaymentGroupDetailUpdate";		
				meta.spDelete = "proc_ParamedicFeePaymentGroupDetailDelete";
				meta.spLoadAll = "proc_ParamedicFeePaymentGroupDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeePaymentGroupDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeePaymentGroupDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
