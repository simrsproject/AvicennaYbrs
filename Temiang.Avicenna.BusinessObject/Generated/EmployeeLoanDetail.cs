/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:15 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esEmployeeLoanDetailCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeLoanDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeLoanDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeLoanDetailQuery query)
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
			this.InitQuery(query as esEmployeeLoanDetailQuery);
		}
		#endregion
		
		virtual public EmployeeLoanDetail DetachEntity(EmployeeLoanDetail entity)
		{
			return base.DetachEntity(entity) as EmployeeLoanDetail;
		}
		
		virtual public EmployeeLoanDetail AttachEntity(EmployeeLoanDetail entity)
		{
			return base.AttachEntity(entity) as EmployeeLoanDetail;
		}
		
		virtual public void Combine(EmployeeLoanDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeLoanDetail this[int index]
		{
			get
			{
				return base[index] as EmployeeLoanDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeLoanDetail);
		}
	}



	[Serializable]
	abstract public class esEmployeeLoanDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeLoanDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeLoanDetail()
		{

		}

		public esEmployeeLoanDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeLoanDetailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLoanDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLoanDetailID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeLoanDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLoanDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLoanDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeLoanDetailID)
		{
			esEmployeeLoanDetailQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeLoanDetailID == employeeLoanDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeLoanDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeLoanDetailID",employeeLoanDetailID);
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
						case "EmployeeLoanDetailID": this.str.EmployeeLoanDetailID = (string)value; break;							
						case "EmployeeLoanID": this.str.EmployeeLoanID = (string)value; break;							
						case "InstallmentNumber": this.str.InstallmentNumber = (string)value; break;							
						case "PlanDate": this.str.PlanDate = (string)value; break;							
						case "PlanAmount": this.str.PlanAmount = (string)value; break;							
						case "MainPayment": this.str.MainPayment = (string)value; break;							
						case "InterestPayment": this.str.InterestPayment = (string)value; break;							
						case "ActualDate": this.str.ActualDate = (string)value; break;							
						case "ActualAmount": this.str.ActualAmount = (string)value; break;							
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;							
						case "IsPaid": this.str.IsPaid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeLoanDetailID":
						
							if (value == null || value is System.Int32)
								this.EmployeeLoanDetailID = (System.Int32?)value;
							break;
						
						case "EmployeeLoanID":
						
							if (value == null || value is System.Int32)
								this.EmployeeLoanID = (System.Int32?)value;
							break;
						
						case "InstallmentNumber":
						
							if (value == null || value is System.Int32)
								this.InstallmentNumber = (System.Int32?)value;
							break;
						
						case "PlanDate":
						
							if (value == null || value is System.DateTime)
								this.PlanDate = (System.DateTime?)value;
							break;
						
						case "PlanAmount":
						
							if (value == null || value is System.Decimal)
								this.PlanAmount = (System.Decimal?)value;
							break;
						
						case "MainPayment":
						
							if (value == null || value is System.Decimal)
								this.MainPayment = (System.Decimal?)value;
							break;
						
						case "InterestPayment":
						
							if (value == null || value is System.Decimal)
								this.InterestPayment = (System.Decimal?)value;
							break;
						
						case "ActualDate":
						
							if (value == null || value is System.DateTime)
								this.ActualDate = (System.DateTime?)value;
							break;
						
						case "ActualAmount":
						
							if (value == null || value is System.Decimal)
								this.ActualAmount = (System.Decimal?)value;
							break;
						
						case "PayrollPeriodID":
						
							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						
						case "IsPaid":
						
							if (value == null || value is System.Boolean)
								this.IsPaid = (System.Boolean?)value;
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
		/// Maps to EmployeeLoanDetail.EmployeeLoanDetailID
		/// </summary>
		virtual public System.Int32? EmployeeLoanDetailID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanDetailID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.EmployeeLoanID
		/// </summary>
		virtual public System.Int32? EmployeeLoanID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.InstallmentNumber
		/// </summary>
		virtual public System.Int32? InstallmentNumber
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.InstallmentNumber);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.InstallmentNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.PlanDate
		/// </summary>
		virtual public System.DateTime? PlanDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLoanDetailMetadata.ColumnNames.PlanDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLoanDetailMetadata.ColumnNames.PlanDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.PlanAmount
		/// </summary>
		virtual public System.Decimal? PlanAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.PlanAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.PlanAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.MainPayment
		/// </summary>
		virtual public System.Decimal? MainPayment
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.MainPayment);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.MainPayment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.InterestPayment
		/// </summary>
		virtual public System.Decimal? InterestPayment
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.InterestPayment);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.InterestPayment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.ActualDate
		/// </summary>
		virtual public System.DateTime? ActualDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLoanDetailMetadata.ColumnNames.ActualDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLoanDetailMetadata.ColumnNames.ActualDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.ActualAmount
		/// </summary>
		virtual public System.Decimal? ActualAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.ActualAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanDetailMetadata.ColumnNames.ActualAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.PayrollPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanDetailMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.IsPaid
		/// </summary>
		virtual public System.Boolean? IsPaid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLoanDetailMetadata.ColumnNames.IsPaid);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeLoanDetailMetadata.ColumnNames.IsPaid, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLoanDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLoanDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoanDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLoanDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeLoanDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esEmployeeLoanDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeLoanDetailID
			{
				get
				{
					System.Int32? data = entity.EmployeeLoanDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLoanDetailID = null;
					else entity.EmployeeLoanDetailID = Convert.ToInt32(value);
				}
			}
				
			public System.String EmployeeLoanID
			{
				get
				{
					System.Int32? data = entity.EmployeeLoanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLoanID = null;
					else entity.EmployeeLoanID = Convert.ToInt32(value);
				}
			}
				
			public System.String InstallmentNumber
			{
				get
				{
					System.Int32? data = entity.InstallmentNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstallmentNumber = null;
					else entity.InstallmentNumber = Convert.ToInt32(value);
				}
			}
				
			public System.String PlanDate
			{
				get
				{
					System.DateTime? data = entity.PlanDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlanDate = null;
					else entity.PlanDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String PlanAmount
			{
				get
				{
					System.Decimal? data = entity.PlanAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlanAmount = null;
					else entity.PlanAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String MainPayment
			{
				get
				{
					System.Decimal? data = entity.MainPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MainPayment = null;
					else entity.MainPayment = Convert.ToDecimal(value);
				}
			}
				
			public System.String InterestPayment
			{
				get
				{
					System.Decimal? data = entity.InterestPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InterestPayment = null;
					else entity.InterestPayment = Convert.ToDecimal(value);
				}
			}
				
			public System.String ActualDate
			{
				get
				{
					System.DateTime? data = entity.ActualDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActualDate = null;
					else entity.ActualDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ActualAmount
			{
				get
				{
					System.Decimal? data = entity.ActualAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActualAmount = null;
					else entity.ActualAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsPaid
			{
				get
				{
					System.Boolean? data = entity.IsPaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaid = null;
					else entity.IsPaid = Convert.ToBoolean(value);
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
			

			private esEmployeeLoanDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeLoanDetailQuery query)
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
				throw new Exception("esEmployeeLoanDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeLoanDetail : esEmployeeLoanDetail
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esEmployeeLoanDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLoanDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeLoanDetailID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanDetailID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeLoanID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem InstallmentNumber
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.InstallmentNumber, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PlanDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.PlanDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PlanAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.PlanAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MainPayment
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.MainPayment, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem InterestPayment
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.InterestPayment, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ActualDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.ActualDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ActualAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.ActualAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsPaid
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.IsPaid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeLoanDetailCollection")]
	public partial class EmployeeLoanDetailCollection : esEmployeeLoanDetailCollection, IEnumerable<EmployeeLoanDetail>
	{
		public EmployeeLoanDetailCollection()
		{

		}
		
		public static implicit operator List<EmployeeLoanDetail>(EmployeeLoanDetailCollection coll)
		{
			List<EmployeeLoanDetail> list = new List<EmployeeLoanDetail>();
			
			foreach (EmployeeLoanDetail emp in coll)
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
				return  EmployeeLoanDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLoanDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeLoanDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeLoanDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeLoanDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLoanDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeLoanDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeLoanDetail AddNew()
		{
			EmployeeLoanDetail entity = base.AddNewEntity() as EmployeeLoanDetail;
			
			return entity;
		}

		public EmployeeLoanDetail FindByPrimaryKey(System.Int32 employeeLoanDetailID)
		{
			return base.FindByPrimaryKey(employeeLoanDetailID) as EmployeeLoanDetail;
		}


		#region IEnumerable<EmployeeLoanDetail> Members

		IEnumerator<EmployeeLoanDetail> IEnumerable<EmployeeLoanDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeLoanDetail;
			}
		}

		#endregion
		
		private EmployeeLoanDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeLoanDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeLoanDetail ({EmployeeLoanDetailID})")]
	[Serializable]
	public partial class EmployeeLoanDetail : esEmployeeLoanDetail
	{
		public EmployeeLoanDetail()
		{

		}
	
		public EmployeeLoanDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLoanDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeLoanDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLoanDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeLoanDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLoanDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeLoanDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeLoanDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeLoanDetailQuery : esEmployeeLoanDetailQuery
	{
		public EmployeeLoanDetailQuery()
		{

		}		
		
		public EmployeeLoanDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeLoanDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeLoanDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeLoanDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.EmployeeLoanDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.EmployeeLoanID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.EmployeeLoanID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.InstallmentNumber, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.InstallmentNumber;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.PlanDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.PlanDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.PlanAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.PlanAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.MainPayment, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.MainPayment;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.InterestPayment, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.InterestPayment;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.ActualDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.ActualDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.ActualAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.ActualAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.PayrollPeriodID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.IsPaid, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.IsPaid;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanDetailMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLoanDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeLoanDetailMetadata Meta()
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
			 public const string EmployeeLoanDetailID = "EmployeeLoanDetailID";
			 public const string EmployeeLoanID = "EmployeeLoanID";
			 public const string InstallmentNumber = "InstallmentNumber";
			 public const string PlanDate = "PlanDate";
			 public const string PlanAmount = "PlanAmount";
			 public const string MainPayment = "MainPayment";
			 public const string InterestPayment = "InterestPayment";
			 public const string ActualDate = "ActualDate";
			 public const string ActualAmount = "ActualAmount";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string IsPaid = "IsPaid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeLoanDetailID = "EmployeeLoanDetailID";
			 public const string EmployeeLoanID = "EmployeeLoanID";
			 public const string InstallmentNumber = "InstallmentNumber";
			 public const string PlanDate = "PlanDate";
			 public const string PlanAmount = "PlanAmount";
			 public const string MainPayment = "MainPayment";
			 public const string InterestPayment = "InterestPayment";
			 public const string ActualDate = "ActualDate";
			 public const string ActualAmount = "ActualAmount";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string IsPaid = "IsPaid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(EmployeeLoanDetailMetadata))
			{
				if(EmployeeLoanDetailMetadata.mapDelegates == null)
				{
					EmployeeLoanDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeLoanDetailMetadata.meta == null)
				{
					EmployeeLoanDetailMetadata.meta = new EmployeeLoanDetailMetadata();
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
				

				meta.AddTypeMap("EmployeeLoanDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeLoanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InstallmentNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PlanDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PlanAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("MainPayment", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("InterestPayment", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ActualDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ActualAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsPaid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeLoanDetail";
				meta.Destination = "EmployeeLoanDetail";
				
				meta.spInsert = "proc_EmployeeLoanDetailInsert";				
				meta.spUpdate = "proc_EmployeeLoanDetailUpdate";		
				meta.spDelete = "proc_EmployeeLoanDetailDelete";
				meta.spLoadAll = "proc_EmployeeLoanDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLoanDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeLoanDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
