/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/18/2020 10:12:18 AM
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
	abstract public class esTransChargesItemConsumptionCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemConsumptionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TransChargesItemConsumptionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTransChargesItemConsumptionQuery query)
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
			this.InitQuery(query as esTransChargesItemConsumptionQuery);
		}
		#endregion
			
		virtual public TransChargesItemConsumption DetachEntity(TransChargesItemConsumption entity)
		{
			return base.DetachEntity(entity) as TransChargesItemConsumption;
		}
		
		virtual public TransChargesItemConsumption AttachEntity(TransChargesItemConsumption entity)
		{
			return base.AttachEntity(entity) as TransChargesItemConsumption;
		}
		
		virtual public void Combine(TransChargesItemConsumptionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesItemConsumption this[int index]
		{
			get
			{
				return base[index] as TransChargesItemConsumption;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemConsumption);
		}
	}

	[Serializable]
	abstract public class esTransChargesItemConsumption : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemConsumptionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTransChargesItemConsumption()
		{
		}
	
		public esTransChargesItemConsumption(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String detailItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, detailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, detailItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String detailItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, detailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, detailItemID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String detailItemID)
		{
			esTransChargesItemConsumptionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo, query.DetailItemID==detailItemID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String detailItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("DetailItemID",detailItemID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "DetailItemID": this.str.DetailItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "QtyRealization": this.str.QtyRealization = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "AveragePrice": this.str.AveragePrice = (string)value; break;
						case "FifoPrice": this.str.FifoPrice = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPackage": this.str.IsPackage = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
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
						case "QtyRealization":
						
							if (value == null || value is System.Decimal)
								this.QtyRealization = (System.Decimal?)value;
							break;
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "AveragePrice":
						
							if (value == null || value is System.Decimal)
								this.AveragePrice = (System.Decimal?)value;
							break;
						case "FifoPrice":
						
							if (value == null || value is System.Decimal)
								this.FifoPrice = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPackage":
						
							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
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
		/// Maps to TransChargesItemConsumption.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.DetailItemID
		/// </summary>
		virtual public System.String DetailItemID
		{
			get
			{
				return base.GetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.DetailItemID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.DetailItemID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.QtyRealization
		/// </summary>
		virtual public System.Decimal? QtyRealization
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.QtyRealization);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.QtyRealization, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.AveragePrice
		/// </summary>
		virtual public System.Decimal? AveragePrice
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.AveragePrice);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.AveragePrice, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.FifoPrice
		/// </summary>
		virtual public System.Decimal? FifoPrice
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.FifoPrice);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemConsumptionMetadata.ColumnNames.FifoPrice, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemConsumptionMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemConsumptionMetadata.ColumnNames.IsPackage, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemConsumption.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.LocationID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemConsumptionMetadata.ColumnNames.LocationID, value);
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
			public esStrings(esTransChargesItemConsumption entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
			public System.String DetailItemID
			{
				get
				{
					System.String data = entity.DetailItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailItemID = null;
					else entity.DetailItemID = Convert.ToString(value);
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
			public System.String QtyRealization
			{
				get
				{
					System.Decimal? data = entity.QtyRealization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyRealization = null;
					else entity.QtyRealization = Convert.ToDecimal(value);
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
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
			public System.String AveragePrice
			{
				get
				{
					System.Decimal? data = entity.AveragePrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AveragePrice = null;
					else entity.AveragePrice = Convert.ToDecimal(value);
				}
			}
			public System.String FifoPrice
			{
				get
				{
					System.Decimal? data = entity.FifoPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FifoPrice = null;
					else entity.FifoPrice = Convert.ToDecimal(value);
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
			public System.String IsPackage
			{
				get
				{
					System.Boolean? data = entity.IsPackage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackage = null;
					else entity.IsPackage = Convert.ToBoolean(value);
				}
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			private esTransChargesItemConsumption entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemConsumptionQuery query)
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
				throw new Exception("esTransChargesItemConsumption can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransChargesItemConsumption : esTransChargesItemConsumption
	{	
	}

	[Serializable]
	abstract public class esTransChargesItemConsumptionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemConsumptionMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem DetailItemID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.DetailItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem QtyRealization
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.QtyRealization, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AveragePrice
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.AveragePrice, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FifoPrice
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.FifoPrice, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemConsumptionMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemConsumptionCollection")]
	public partial class TransChargesItemConsumptionCollection : esTransChargesItemConsumptionCollection, IEnumerable< TransChargesItemConsumption>
	{
		public TransChargesItemConsumptionCollection()
		{

		}	
		
		public static implicit operator List< TransChargesItemConsumption>(TransChargesItemConsumptionCollection coll)
		{
			List< TransChargesItemConsumption> list = new List< TransChargesItemConsumption>();
			
			foreach (TransChargesItemConsumption emp in coll)
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
				return  TransChargesItemConsumptionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemConsumption(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemConsumption();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TransChargesItemConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemConsumptionQuery();
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
		public bool Load(TransChargesItemConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransChargesItemConsumption AddNew()
		{
			TransChargesItemConsumption entity = base.AddNewEntity() as TransChargesItemConsumption;
			
			return entity;		
		}
		public TransChargesItemConsumption FindByPrimaryKey(String transactionNo, String sequenceNo, String detailItemID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, detailItemID) as TransChargesItemConsumption;
		}

		#region IEnumerable< TransChargesItemConsumption> Members

		IEnumerator< TransChargesItemConsumption> IEnumerable< TransChargesItemConsumption>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemConsumption;
			}
		}

		#endregion
		
		private TransChargesItemConsumptionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemConsumption' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransChargesItemConsumption ({TransactionNo, SequenceNo, DetailItemID})")]
	[Serializable]
	public partial class TransChargesItemConsumption : esTransChargesItemConsumption
	{
		public TransChargesItemConsumption()
		{
		}	
	
		public TransChargesItemConsumption(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemConsumptionMetadata.Meta();
			}
		}	
	
		override protected esTransChargesItemConsumptionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TransChargesItemConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemConsumptionQuery();
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
		public bool Load(TransChargesItemConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TransChargesItemConsumptionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransChargesItemConsumptionQuery : esTransChargesItemConsumptionQuery
	{
		public TransChargesItemConsumptionQuery()
		{

		}		
		
		public TransChargesItemConsumptionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TransChargesItemConsumptionQuery";
        }
	}

	[Serializable]
	public partial class TransChargesItemConsumptionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemConsumptionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 12;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.DetailItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.DetailItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.QtyRealization, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.QtyRealization;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.Price, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.AveragePrice, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.AveragePrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.FifoPrice, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.FifoPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.IsPackage, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.IsPackage;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesItemConsumptionMetadata.ColumnNames.LocationID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemConsumptionMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TransChargesItemConsumptionMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string DetailItemID = "DetailItemID";
			public const string Qty = "Qty";
			public const string QtyRealization = "QtyRealization";
			public const string SRItemUnit = "SRItemUnit";
			public const string Price = "Price";
			public const string AveragePrice = "AveragePrice";
			public const string FifoPrice = "FifoPrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPackage = "IsPackage";
			public const string LocationID = "LocationID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string DetailItemID = "DetailItemID";
			public const string Qty = "Qty";
			public const string QtyRealization = "QtyRealization";
			public const string SRItemUnit = "SRItemUnit";
			public const string Price = "Price";
			public const string AveragePrice = "AveragePrice";
			public const string FifoPrice = "FifoPrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPackage = "IsPackage";
			public const string LocationID = "LocationID";
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
			lock (typeof(TransChargesItemConsumptionMetadata))
			{
				if(TransChargesItemConsumptionMetadata.mapDelegates == null)
				{
					TransChargesItemConsumptionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesItemConsumptionMetadata.meta == null)
				{
					TransChargesItemConsumptionMetadata.meta = new TransChargesItemConsumptionMetadata();
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
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DetailItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyRealization", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AveragePrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FifoPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "TransChargesItemConsumption";
				meta.Destination = "TransChargesItemConsumption";
				meta.spInsert = "proc_TransChargesItemConsumptionInsert";				
				meta.spUpdate = "proc_TransChargesItemConsumptionUpdate";		
				meta.spDelete = "proc_TransChargesItemConsumptionDelete";
				meta.spLoadAll = "proc_TransChargesItemConsumptionLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemConsumptionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemConsumptionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
