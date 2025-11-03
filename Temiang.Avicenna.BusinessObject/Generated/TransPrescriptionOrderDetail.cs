/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/17/2020 8:38:19 AM
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
	abstract public class esTransPrescriptionOrderDetailCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionOrderDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TransPrescriptionOrderDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTransPrescriptionOrderDetailQuery query)
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
			this.InitQuery(query as esTransPrescriptionOrderDetailQuery);
		}
		#endregion
			
		virtual public TransPrescriptionOrderDetail DetachEntity(TransPrescriptionOrderDetail entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionOrderDetail;
		}
		
		virtual public TransPrescriptionOrderDetail AttachEntity(TransPrescriptionOrderDetail entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionOrderDetail;
		}
		
		virtual public void Combine(TransPrescriptionOrderDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPrescriptionOrderDetail this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionOrderDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionOrderDetail);
		}
	}

	[Serializable]
	abstract public class esTransPrescriptionOrderDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionOrderDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTransPrescriptionOrderDetail()
		{
		}
	
		public esTransPrescriptionOrderDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo, String prescriptionNo, String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, prescriptionNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo, String prescriptionNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, prescriptionNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String orderNo, String prescriptionNo, String sequenceNo)
		{
			esTransPrescriptionOrderDetailQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo==orderNo, query.PrescriptionNo==prescriptionNo, query.SequenceNo==sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String orderNo, String prescriptionNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo",orderNo);
			parms.Add("PrescriptionNo",prescriptionNo);
			parms.Add("SequenceNo",sequenceNo);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateBy": this.str.CreateBy = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateBy": this.str.LastUpdateBy = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
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
		/// Maps to TransPrescriptionOrderDetail.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.PrescriptionNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionOrderDetailMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionOrderDetailMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionOrderDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionOrderDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.CreateBy
		/// </summary>
		virtual public System.String CreateBy
		{
			get
			{
				return base.GetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.CreateBy);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.CreateBy, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionOrderDetail.LastUpdateBy
		/// </summary>
		virtual public System.String LastUpdateBy
		{
			get
			{
				return base.GetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateBy);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateBy, value);
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
			public esStrings(esTransPrescriptionOrderDetail entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String PrescriptionNo
			{
				get
				{
					System.String data = entity.PrescriptionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionNo = null;
					else entity.PrescriptionNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
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
			public System.String CreateBy
			{
				get
				{
					System.String data = entity.CreateBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateBy = null;
					else entity.CreateBy = Convert.ToString(value);
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
			public System.String LastUpdateBy
			{
				get
				{
					System.String data = entity.LastUpdateBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateBy = null;
					else entity.LastUpdateBy = Convert.ToString(value);
				}
			}
			private esTransPrescriptionOrderDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionOrderDetailQuery query)
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
				throw new Exception("esTransPrescriptionOrderDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPrescriptionOrderDetail : esTransPrescriptionOrderDetail
	{	
	}

	[Serializable]
	abstract public class esTransPrescriptionOrderDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionOrderDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateBy
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.CreateBy, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateBy
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateBy, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionOrderDetailCollection")]
	public partial class TransPrescriptionOrderDetailCollection : esTransPrescriptionOrderDetailCollection, IEnumerable< TransPrescriptionOrderDetail>
	{
		public TransPrescriptionOrderDetailCollection()
		{

		}	
		
		public static implicit operator List< TransPrescriptionOrderDetail>(TransPrescriptionOrderDetailCollection coll)
		{
			List< TransPrescriptionOrderDetail> list = new List< TransPrescriptionOrderDetail>();
			
			foreach (TransPrescriptionOrderDetail emp in coll)
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
				return  TransPrescriptionOrderDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionOrderDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionOrderDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionOrderDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TransPrescriptionOrderDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionOrderDetailQuery();
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
		public bool Load(TransPrescriptionOrderDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPrescriptionOrderDetail AddNew()
		{
			TransPrescriptionOrderDetail entity = base.AddNewEntity() as TransPrescriptionOrderDetail;
			
			return entity;		
		}
		public TransPrescriptionOrderDetail FindByPrimaryKey(String orderNo, String prescriptionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(orderNo, prescriptionNo, sequenceNo) as TransPrescriptionOrderDetail;
		}

		#region IEnumerable< TransPrescriptionOrderDetail> Members

		IEnumerator< TransPrescriptionOrderDetail> IEnumerable< TransPrescriptionOrderDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionOrderDetail;
			}
		}

		#endregion
		
		private TransPrescriptionOrderDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionOrderDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPrescriptionOrderDetail ({OrderNo, PrescriptionNo, SequenceNo})")]
	[Serializable]
	public partial class TransPrescriptionOrderDetail : esTransPrescriptionOrderDetail
	{
		public TransPrescriptionOrderDetail()
		{
		}	
	
		public TransPrescriptionOrderDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionOrderDetailMetadata.Meta();
			}
		}	
	
		override protected esTransPrescriptionOrderDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionOrderDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TransPrescriptionOrderDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionOrderDetailQuery();
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
		public bool Load(TransPrescriptionOrderDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TransPrescriptionOrderDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPrescriptionOrderDetailQuery : esTransPrescriptionOrderDetailQuery
	{
		public TransPrescriptionOrderDetailQuery()
		{

		}		
		
		public TransPrescriptionOrderDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TransPrescriptionOrderDetailQuery";
        }
	}

	[Serializable]
	public partial class TransPrescriptionOrderDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionOrderDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.PrescriptionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.CreateBy, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.CreateBy;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPrescriptionOrderDetailMetadata.ColumnNames.LastUpdateBy, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionOrderDetailMetadata.PropertyNames.LastUpdateBy;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TransPrescriptionOrderDetailMetadata Meta()
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
			public const string OrderNo = "OrderNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string SequenceNo = "SequenceNo";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateBy = "CreateBy";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateBy = "LastUpdateBy";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string OrderNo = "OrderNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string SequenceNo = "SequenceNo";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateBy = "CreateBy";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateBy = "LastUpdateBy";
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
			lock (typeof(TransPrescriptionOrderDetailMetadata))
			{
				if(TransPrescriptionOrderDetailMetadata.mapDelegates == null)
				{
					TransPrescriptionOrderDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPrescriptionOrderDetailMetadata.meta == null)
				{
					TransPrescriptionOrderDetailMetadata.meta = new TransPrescriptionOrderDetailMetadata();
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
				
				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateBy", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "TransPrescriptionOrderDetail";
				meta.Destination = "TransPrescriptionOrderDetail";
				meta.spInsert = "proc_TransPrescriptionOrderDetailInsert";				
				meta.spUpdate = "proc_TransPrescriptionOrderDetailUpdate";		
				meta.spDelete = "proc_TransPrescriptionOrderDetailDelete";
				meta.spLoadAll = "proc_TransPrescriptionOrderDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionOrderDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionOrderDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
