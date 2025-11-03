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
	abstract public class esTransPaymentItemCorrectionCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentItemCorrectionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TransPaymentItemCorrectionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTransPaymentItemCorrectionQuery query)
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
			this.InitQuery(query as esTransPaymentItemCorrectionQuery);
		}
		#endregion
			
		virtual public TransPaymentItemCorrection DetachEntity(TransPaymentItemCorrection entity)
		{
			return base.DetachEntity(entity) as TransPaymentItemCorrection;
		}
		
		virtual public TransPaymentItemCorrection AttachEntity(TransPaymentItemCorrection entity)
		{
			return base.AttachEntity(entity) as TransPaymentItemCorrection;
		}
		
		virtual public void Combine(TransPaymentItemCorrectionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPaymentItemCorrection this[int index]
		{
			get
			{
				return base[index] as TransPaymentItemCorrection;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentItemCorrection);
		}
	}

	[Serializable]
	abstract public class esTransPaymentItemCorrection : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentItemCorrectionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTransPaymentItemCorrection()
		{
		}
	
		public esTransPaymentItemCorrection(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentCorrectionNo, String paymentNo, String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentCorrectionNo, paymentNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentCorrectionNo, paymentNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentCorrectionNo, String paymentNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentCorrectionNo, paymentNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentCorrectionNo, paymentNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paymentCorrectionNo, String paymentNo, String sequenceNo)
		{
			esTransPaymentItemCorrectionQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentCorrectionNo==paymentCorrectionNo, query.PaymentNo==paymentNo, query.SequenceNo==sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paymentCorrectionNo, String paymentNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentCorrectionNo",paymentCorrectionNo);
			parms.Add("PaymentNo",paymentNo);
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
						case "PaymentCorrectionNo": this.str.PaymentCorrectionNo = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;
						case "SRCardType": this.str.SRCardType = (string)value; break;
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;
						case "CardFeeAmount": this.str.CardFeeAmount = (string)value; break;
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
						case "CardFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.CardFeeAmount = (System.Decimal?)value;
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
		/// Maps to TransPaymentItemCorrection.PaymentCorrectionNo
		/// </summary>
		virtual public System.String PaymentCorrectionNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.PaymentCorrectionNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.PaymentCorrectionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentType);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentMethod);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.SRCardProvider
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRCardProvider);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRCardType);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.SRCardType, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.EDCMachineID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.CardFeeAmount
		/// </summary>
		virtual public System.Decimal? CardFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemCorrectionMetadata.ColumnNames.CardFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPaymentItemCorrectionMetadata.ColumnNames.CardFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentItemCorrectionMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentItemCorrectionMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemCorrection.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esTransPaymentItemCorrection entity)
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
			public System.String SRPaymentType
			{
				get
				{
					System.String data = entity.SRPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentType = null;
					else entity.SRPaymentType = Convert.ToString(value);
				}
			}
			public System.String SRPaymentMethod
			{
				get
				{
					System.String data = entity.SRPaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentMethod = null;
					else entity.SRPaymentMethod = Convert.ToString(value);
				}
			}
			public System.String SRCardProvider
			{
				get
				{
					System.String data = entity.SRCardProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardProvider = null;
					else entity.SRCardProvider = Convert.ToString(value);
				}
			}
			public System.String SRCardType
			{
				get
				{
					System.String data = entity.SRCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardType = null;
					else entity.SRCardType = Convert.ToString(value);
				}
			}
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
			public System.String CardFeeAmount
			{
				get
				{
					System.Decimal? data = entity.CardFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardFeeAmount = null;
					else entity.CardFeeAmount = Convert.ToDecimal(value);
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
			private esTransPaymentItemCorrection entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentItemCorrectionQuery query)
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
				throw new Exception("esTransPaymentItemCorrection can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentItemCorrection : esTransPaymentItemCorrection
	{	
	}

	[Serializable]
	abstract public class esTransPaymentItemCorrectionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemCorrectionMetadata.Meta();
			}
		}	
			
		public esQueryItem PaymentCorrectionNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.PaymentCorrectionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		} 
			
		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		} 
			
		public esQueryItem CardFeeAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.CardFeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentItemCorrectionCollection")]
	public partial class TransPaymentItemCorrectionCollection : esTransPaymentItemCorrectionCollection, IEnumerable< TransPaymentItemCorrection>
	{
		public TransPaymentItemCorrectionCollection()
		{

		}	
		
		public static implicit operator List< TransPaymentItemCorrection>(TransPaymentItemCorrectionCollection coll)
		{
			List< TransPaymentItemCorrection> list = new List< TransPaymentItemCorrection>();
			
			foreach (TransPaymentItemCorrection emp in coll)
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
				return  TransPaymentItemCorrectionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentItemCorrection(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentItemCorrection();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TransPaymentItemCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemCorrectionQuery();
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
		public bool Load(TransPaymentItemCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentItemCorrection AddNew()
		{
			TransPaymentItemCorrection entity = base.AddNewEntity() as TransPaymentItemCorrection;
			
			return entity;		
		}
		public TransPaymentItemCorrection FindByPrimaryKey(String paymentCorrectionNo, String paymentNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(paymentCorrectionNo, paymentNo, sequenceNo) as TransPaymentItemCorrection;
		}

		#region IEnumerable< TransPaymentItemCorrection> Members

		IEnumerator< TransPaymentItemCorrection> IEnumerable< TransPaymentItemCorrection>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentItemCorrection;
			}
		}

		#endregion
		
		private TransPaymentItemCorrectionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentItemCorrection' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentItemCorrection ({PaymentCorrectionNo, PaymentNo, SequenceNo})")]
	[Serializable]
	public partial class TransPaymentItemCorrection : esTransPaymentItemCorrection
	{
		public TransPaymentItemCorrection()
		{
		}	
	
		public TransPaymentItemCorrection(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemCorrectionMetadata.Meta();
			}
		}	
	
		override protected esTransPaymentItemCorrectionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TransPaymentItemCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemCorrectionQuery();
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
		public bool Load(TransPaymentItemCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TransPaymentItemCorrectionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentItemCorrectionQuery : esTransPaymentItemCorrectionQuery
	{
		public TransPaymentItemCorrectionQuery()
		{

		}		
		
		public TransPaymentItemCorrectionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TransPaymentItemCorrectionQuery";
        }
	}

	[Serializable]
	public partial class TransPaymentItemCorrectionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentItemCorrectionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.PaymentCorrectionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.PaymentCorrectionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.SRPaymentMethod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.SRCardProvider, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.SRCardType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.SRCardType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.EDCMachineID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.EDCMachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.CardFeeAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.CardFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransPaymentItemCorrectionMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentItemCorrectionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TransPaymentItemCorrectionMetadata Meta()
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
			public const string PaymentNo = "PaymentNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardFeeAmount = "CardFeeAmount";
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
			public const string PaymentNo = "PaymentNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardFeeAmount = "CardFeeAmount";
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
			lock (typeof(TransPaymentItemCorrectionMetadata))
			{
				if(TransPaymentItemCorrectionMetadata.mapDelegates == null)
				{
					TransPaymentItemCorrectionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPaymentItemCorrectionMetadata.meta == null)
				{
					TransPaymentItemCorrectionMetadata.meta = new TransPaymentItemCorrectionMetadata();
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
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "TransPaymentItemCorrection";
				meta.Destination = "TransPaymentItemCorrection";
				meta.spInsert = "proc_TransPaymentItemCorrectionInsert";				
				meta.spUpdate = "proc_TransPaymentItemCorrectionUpdate";		
				meta.spDelete = "proc_TransPaymentItemCorrectionDelete";
				meta.spLoadAll = "proc_TransPaymentItemCorrectionLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentItemCorrectionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentItemCorrectionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
