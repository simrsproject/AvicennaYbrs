/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/12/2018 7:59:24 AM
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
	abstract public class esTransPaymentCorrectionCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentCorrectionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TransPaymentCorrectionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTransPaymentCorrectionQuery query)
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
			this.InitQuery(query as esTransPaymentCorrectionQuery);
		}
		#endregion
			
		virtual public TransPaymentCorrection DetachEntity(TransPaymentCorrection entity)
		{
			return base.DetachEntity(entity) as TransPaymentCorrection;
		}
		
		virtual public TransPaymentCorrection AttachEntity(TransPaymentCorrection entity)
		{
			return base.AttachEntity(entity) as TransPaymentCorrection;
		}
		
		virtual public void Combine(TransPaymentCorrectionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPaymentCorrection this[int index]
		{
			get
			{
				return base[index] as TransPaymentCorrection;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentCorrection);
		}
	}

	[Serializable]
	abstract public class esTransPaymentCorrection : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentCorrectionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTransPaymentCorrection()
		{
		}
	
		public esTransPaymentCorrection(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentCorrectionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentCorrectionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentCorrectionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentCorrectionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentCorrectionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentCorrectionNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paymentCorrectionNo)
		{
			esTransPaymentCorrectionQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentCorrectionNo==paymentCorrectionNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paymentCorrectionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentCorrectionNo",paymentCorrectionNo);
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
						case "PaymentCorrectionNo": this.str.PaymentCorrectionNo = (string)value; break;
						case "PaymentCorrectionDate": this.str.PaymentCorrectionDate = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "DateApproved": this.str.DateApproved = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "DateVoid": this.str.DateVoid = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PaymentCorrectionDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentCorrectionDate = (System.DateTime?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "DateApproved":
						
							if (value == null || value is System.DateTime)
								this.DateApproved = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "DateVoid":
						
							if (value == null || value is System.DateTime)
								this.DateVoid = (System.DateTime?)value;
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
		/// Maps to TransPaymentCorrection.PaymentCorrectionNo
		/// </summary>
		virtual public System.String PaymentCorrectionNo
		{
			get
			{
				return base.GetSystemString(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.PaymentCorrectionDate
		/// </summary>
		virtual public System.DateTime? PaymentCorrectionDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentCorrectionMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(TransPaymentCorrectionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.DateApproved
		/// </summary>
		virtual public System.DateTime? DateApproved
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.DateApproved);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.DateApproved, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentCorrectionMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentCorrectionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentCorrectionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransPaymentCorrectionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentCorrectionMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentCorrectionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.DateVoid
		/// </summary>
		virtual public System.DateTime? DateVoid
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.DateVoid);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.DateVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentCorrectionMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentCorrectionMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentCorrectionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentCorrectionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentCorrection.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentCorrectionMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esTransPaymentCorrection entity)
			{
				this.entity = entity;
			}
			public System.String PaymentCorrectionNo
			{
				get
				{
					System.String data = entity.PaymentCorrectionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentCorrectionNo = null;
					else entity.PaymentCorrectionNo = Convert.ToString(value);
				}
			}
			public System.String PaymentCorrectionDate
			{
				get
				{
					System.DateTime? data = entity.PaymentCorrectionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentCorrectionDate = null;
					else entity.PaymentCorrectionDate = Convert.ToDateTime(value);
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
			public System.String DateApproved
			{
				get
				{
					System.DateTime? data = entity.DateApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateApproved = null;
					else entity.DateApproved = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
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
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String DateVoid
			{
				get
				{
					System.DateTime? data = entity.DateVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateVoid = null;
					else entity.DateVoid = Convert.ToDateTime(value);
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
			private esTransPaymentCorrection entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentCorrectionQuery query)
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
				throw new Exception("esTransPaymentCorrection can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentCorrection : esTransPaymentCorrection
	{	
	}

	[Serializable]
	abstract public class esTransPaymentCorrectionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentCorrectionMetadata.Meta();
			}
		}	
			
		public esQueryItem PaymentCorrectionNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentCorrectionDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem DateApproved
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.DateApproved, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem DateVoid
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.DateVoid, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentCorrectionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentCorrectionCollection")]
	public partial class TransPaymentCorrectionCollection : esTransPaymentCorrectionCollection, IEnumerable< TransPaymentCorrection>
	{
		public TransPaymentCorrectionCollection()
		{

		}	
		
		public static implicit operator List< TransPaymentCorrection>(TransPaymentCorrectionCollection coll)
		{
			List< TransPaymentCorrection> list = new List< TransPaymentCorrection>();
			
			foreach (TransPaymentCorrection emp in coll)
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
				return  TransPaymentCorrectionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentCorrection(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentCorrection();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TransPaymentCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentCorrectionQuery();
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
		public bool Load(TransPaymentCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentCorrection AddNew()
		{
			TransPaymentCorrection entity = base.AddNewEntity() as TransPaymentCorrection;
			
			return entity;		
		}
		public TransPaymentCorrection FindByPrimaryKey(String paymentCorrectionNo)
		{
			return base.FindByPrimaryKey(paymentCorrectionNo) as TransPaymentCorrection;
		}

		#region IEnumerable< TransPaymentCorrection> Members

		IEnumerator< TransPaymentCorrection> IEnumerable< TransPaymentCorrection>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentCorrection;
			}
		}

		#endregion
		
		private TransPaymentCorrectionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentCorrection' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentCorrection ({PaymentCorrectionNo})")]
	[Serializable]
	public partial class TransPaymentCorrection : esTransPaymentCorrection
	{
		public TransPaymentCorrection()
		{
		}	
	
		public TransPaymentCorrection(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentCorrectionMetadata.Meta();
			}
		}	
	
		override protected esTransPaymentCorrectionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TransPaymentCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentCorrectionQuery();
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
		public bool Load(TransPaymentCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TransPaymentCorrectionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentCorrectionQuery : esTransPaymentCorrectionQuery
	{
		public TransPaymentCorrectionQuery()
		{

		}		
		
		public TransPaymentCorrectionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TransPaymentCorrectionQuery";
        }
	}

	[Serializable]
	public partial class TransPaymentCorrectionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentCorrectionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.PaymentCorrectionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.PaymentCorrectionDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.IsApproved, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.IsApproved;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.DateApproved, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.DateApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.ApprovedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.IsVoid, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.VoidByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.DateVoid, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.DateVoid;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentCorrectionMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentCorrectionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TransPaymentCorrectionMetadata Meta()
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
			public const string PaymentCorrectionNo = "PaymentCorrectionNo";
			public const string PaymentCorrectionDate = "PaymentCorrectionDate";
			public const string IsApproved = "IsApproved";
			public const string DateApproved = "DateApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string DateVoid = "DateVoid";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PaymentCorrectionNo = "PaymentCorrectionNo";
			public const string PaymentCorrectionDate = "PaymentCorrectionDate";
			public const string IsApproved = "IsApproved";
			public const string DateApproved = "DateApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string DateVoid = "DateVoid";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
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
			lock (typeof(TransPaymentCorrectionMetadata))
			{
				if(TransPaymentCorrectionMetadata.mapDelegates == null)
				{
					TransPaymentCorrectionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPaymentCorrectionMetadata.meta == null)
				{
					TransPaymentCorrectionMetadata.meta = new TransPaymentCorrectionMetadata();
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
				
				meta.AddTypeMap("PaymentCorrectionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentCorrectionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DateApproved", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateVoid", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "TransPaymentCorrection";
				meta.Destination = "TransPaymentCorrection";
				meta.spInsert = "proc_TransPaymentCorrectionInsert";				
				meta.spUpdate = "proc_TransPaymentCorrectionUpdate";		
				meta.spDelete = "proc_TransPaymentCorrectionDelete";
				meta.spLoadAll = "proc_TransPaymentCorrectionLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentCorrectionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentCorrectionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
