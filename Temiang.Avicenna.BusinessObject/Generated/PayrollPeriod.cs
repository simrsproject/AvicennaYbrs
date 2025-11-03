/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/25/2016 3:40:38 AM
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
	abstract public class esPayrollPeriodCollection : esEntityCollectionWAuditLog
	{
		public esPayrollPeriodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PayrollPeriodCollection";
		}

		#region Query Logic
		protected void InitQuery(esPayrollPeriodQuery query)
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
			this.InitQuery(query as esPayrollPeriodQuery);
		}
		#endregion
		
		virtual public PayrollPeriod DetachEntity(PayrollPeriod entity)
		{
			return base.DetachEntity(entity) as PayrollPeriod;
		}
		
		virtual public PayrollPeriod AttachEntity(PayrollPeriod entity)
		{
			return base.AttachEntity(entity) as PayrollPeriod;
		}
		
		virtual public void Combine(PayrollPeriodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PayrollPeriod this[int index]
		{
			get
			{
				return base[index] as PayrollPeriod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PayrollPeriod);
		}
	}



	[Serializable]
	abstract public class esPayrollPeriod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPayrollPeriodQuery GetDynamicQuery()
		{
			return null;
		}

		public esPayrollPeriod()
		{

		}

		public esPayrollPeriod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 payrollPeriodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(payrollPeriodID);
			else
				return LoadByPrimaryKeyStoredProcedure(payrollPeriodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 payrollPeriodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(payrollPeriodID);
			else
				return LoadByPrimaryKeyStoredProcedure(payrollPeriodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 payrollPeriodID)
		{
			esPayrollPeriodQuery query = this.GetDynamicQuery();
			query.Where(query.PayrollPeriodID == payrollPeriodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 payrollPeriodID)
		{
			esParameters parms = new esParameters();
			parms.Add("PayrollPeriodID",payrollPeriodID);
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
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;							
						case "PayrollPeriodCode": this.str.PayrollPeriodCode = (string)value; break;							
						case "PayrollPeriodName": this.str.PayrollPeriodName = (string)value; break;							
						case "SRPaySequent": this.str.SRPaySequent = (string)value; break;							
						case "StartDate": this.str.StartDate = (string)value; break;							
						case "EndDate": this.str.EndDate = (string)value; break;							
						case "PayDate": this.str.PayDate = (string)value; break;							
						case "SPTYear": this.str.SPTYear = (string)value; break;							
						case "SPTMonth": this.str.SPTMonth = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "WageProcessTypeID": this.str.WageProcessTypeID = (string)value; break;							
						case "IsMoslemTHR": this.str.IsMoslemTHR = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PayrollPeriodID":
						
							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						
						case "StartDate":
						
							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						
						case "EndDate":
						
							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						
						case "PayDate":
						
							if (value == null || value is System.DateTime)
								this.PayDate = (System.DateTime?)value;
							break;
						
						case "SPTYear":
						
							if (value == null || value is System.Int32)
								this.SPTYear = (System.Int32?)value;
							break;
						
						case "SPTMonth":
						
							if (value == null || value is System.Int32)
								this.SPTMonth = (System.Int32?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "WageProcessTypeID":
						
							if (value == null || value is System.Int32)
								this.WageProcessTypeID = (System.Int32?)value;
							break;
						
						case "IsMoslemTHR":
						
							if (value == null || value is System.Boolean)
								this.IsMoslemTHR = (System.Boolean?)value;
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
		/// Maps to PayrollPeriod.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(PayrollPeriodMetadata.ColumnNames.PayrollPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(PayrollPeriodMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.PayrollPeriodCode
		/// </summary>
		virtual public System.String PayrollPeriodCode
		{
			get
			{
				return base.GetSystemString(PayrollPeriodMetadata.ColumnNames.PayrollPeriodCode);
			}
			
			set
			{
				base.SetSystemString(PayrollPeriodMetadata.ColumnNames.PayrollPeriodCode, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.PayrollPeriodName
		/// </summary>
		virtual public System.String PayrollPeriodName
		{
			get
			{
				return base.GetSystemString(PayrollPeriodMetadata.ColumnNames.PayrollPeriodName);
			}
			
			set
			{
				base.SetSystemString(PayrollPeriodMetadata.ColumnNames.PayrollPeriodName, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.SRPaySequent
		/// </summary>
		virtual public System.String SRPaySequent
		{
			get
			{
				return base.GetSystemString(PayrollPeriodMetadata.ColumnNames.SRPaySequent);
			}
			
			set
			{
				base.SetSystemString(PayrollPeriodMetadata.ColumnNames.SRPaySequent, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(PayrollPeriodMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemDateTime(PayrollPeriodMetadata.ColumnNames.StartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(PayrollPeriodMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemDateTime(PayrollPeriodMetadata.ColumnNames.EndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.PayDate
		/// </summary>
		virtual public System.DateTime? PayDate
		{
			get
			{
				return base.GetSystemDateTime(PayrollPeriodMetadata.ColumnNames.PayDate);
			}
			
			set
			{
				base.SetSystemDateTime(PayrollPeriodMetadata.ColumnNames.PayDate, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.SPTYear
		/// </summary>
		virtual public System.Int32? SPTYear
		{
			get
			{
				return base.GetSystemInt32(PayrollPeriodMetadata.ColumnNames.SPTYear);
			}
			
			set
			{
				base.SetSystemInt32(PayrollPeriodMetadata.ColumnNames.SPTYear, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.SPTMonth
		/// </summary>
		virtual public System.Int32? SPTMonth
		{
			get
			{
				return base.GetSystemInt32(PayrollPeriodMetadata.ColumnNames.SPTMonth);
			}
			
			set
			{
				base.SetSystemInt32(PayrollPeriodMetadata.ColumnNames.SPTMonth, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PayrollPeriodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PayrollPeriodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PayrollPeriodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PayrollPeriodMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.WageProcessTypeID
		/// </summary>
		virtual public System.Int32? WageProcessTypeID
		{
			get
			{
				return base.GetSystemInt32(PayrollPeriodMetadata.ColumnNames.WageProcessTypeID);
			}
			
			set
			{
				base.SetSystemInt32(PayrollPeriodMetadata.ColumnNames.WageProcessTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to PayrollPeriod.IsMoslemTHR
		/// </summary>
		virtual public System.Boolean? IsMoslemTHR
		{
			get
			{
				return base.GetSystemBoolean(PayrollPeriodMetadata.ColumnNames.IsMoslemTHR);
			}
			
			set
			{
				base.SetSystemBoolean(PayrollPeriodMetadata.ColumnNames.IsMoslemTHR, value);
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
			public esStrings(esPayrollPeriod entity)
			{
				this.entity = entity;
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
				
			public System.String PayrollPeriodCode
			{
				get
				{
					System.String data = entity.PayrollPeriodCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodCode = null;
					else entity.PayrollPeriodCode = Convert.ToString(value);
				}
			}
				
			public System.String PayrollPeriodName
			{
				get
				{
					System.String data = entity.PayrollPeriodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodName = null;
					else entity.PayrollPeriodName = Convert.ToString(value);
				}
			}
				
			public System.String SRPaySequent
			{
				get
				{
					System.String data = entity.SRPaySequent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaySequent = null;
					else entity.SRPaySequent = Convert.ToString(value);
				}
			}
				
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String PayDate
			{
				get
				{
					System.DateTime? data = entity.PayDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayDate = null;
					else entity.PayDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SPTYear
			{
				get
				{
					System.Int32? data = entity.SPTYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPTYear = null;
					else entity.SPTYear = Convert.ToInt32(value);
				}
			}
				
			public System.String SPTMonth
			{
				get
				{
					System.Int32? data = entity.SPTMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPTMonth = null;
					else entity.SPTMonth = Convert.ToInt32(value);
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
				
			public System.String WageProcessTypeID
			{
				get
				{
					System.Int32? data = entity.WageProcessTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageProcessTypeID = null;
					else entity.WageProcessTypeID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsMoslemTHR
			{
				get
				{
					System.Boolean? data = entity.IsMoslemTHR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMoslemTHR = null;
					else entity.IsMoslemTHR = Convert.ToBoolean(value);
				}
			}
			

			private esPayrollPeriod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPayrollPeriodQuery query)
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
				throw new Exception("esPayrollPeriod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PayrollPeriod : esPayrollPeriod
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
	abstract public class esPayrollPeriodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PayrollPeriodMetadata.Meta();
			}
		}	
		

		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PayrollPeriodCode
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.PayrollPeriodCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PayrollPeriodName
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.PayrollPeriodName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPaySequent
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.SRPaySequent, esSystemType.String);
			}
		} 
		
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PayDate
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.PayDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SPTYear
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.SPTYear, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SPTMonth
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.SPTMonth, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem WageProcessTypeID
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.WageProcessTypeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsMoslemTHR
		{
			get
			{
				return new esQueryItem(this, PayrollPeriodMetadata.ColumnNames.IsMoslemTHR, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PayrollPeriodCollection")]
	public partial class PayrollPeriodCollection : esPayrollPeriodCollection, IEnumerable<PayrollPeriod>
	{
		public PayrollPeriodCollection()
		{

		}
		
		public static implicit operator List<PayrollPeriod>(PayrollPeriodCollection coll)
		{
			List<PayrollPeriod> list = new List<PayrollPeriod>();
			
			foreach (PayrollPeriod emp in coll)
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
				return  PayrollPeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PayrollPeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PayrollPeriod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PayrollPeriod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PayrollPeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PayrollPeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PayrollPeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PayrollPeriod AddNew()
		{
			PayrollPeriod entity = base.AddNewEntity() as PayrollPeriod;
			
			return entity;
		}

		public PayrollPeriod FindByPrimaryKey(System.Int32 payrollPeriodID)
		{
			return base.FindByPrimaryKey(payrollPeriodID) as PayrollPeriod;
		}


		#region IEnumerable<PayrollPeriod> Members

		IEnumerator<PayrollPeriod> IEnumerable<PayrollPeriod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PayrollPeriod;
			}
		}

		#endregion
		
		private PayrollPeriodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PayrollPeriod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PayrollPeriod ({PayrollPeriodID})")]
	[Serializable]
	public partial class PayrollPeriod : esPayrollPeriod
	{
		public PayrollPeriod()
		{

		}
	
		public PayrollPeriod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PayrollPeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esPayrollPeriodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PayrollPeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PayrollPeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PayrollPeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PayrollPeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PayrollPeriodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PayrollPeriodQuery : esPayrollPeriodQuery
	{
		public PayrollPeriodQuery()
		{

		}		
		
		public PayrollPeriodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PayrollPeriodQuery";
        }
		
			
	}


	[Serializable]
	public partial class PayrollPeriodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PayrollPeriodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.PayrollPeriodID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.PayrollPeriodID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.PayrollPeriodCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.PayrollPeriodCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.PayrollPeriodName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.PayrollPeriodName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.SRPaySequent, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.SRPaySequent;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.StartDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.StartDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.EndDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.EndDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.PayDate, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.PayDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.SPTYear, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.SPTYear;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.SPTMonth, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.SPTMonth;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.WageProcessTypeID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.WageProcessTypeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PayrollPeriodMetadata.ColumnNames.IsMoslemTHR, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PayrollPeriodMetadata.PropertyNames.IsMoslemTHR;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PayrollPeriodMetadata Meta()
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
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string PayrollPeriodCode = "PayrollPeriodCode";
			 public const string PayrollPeriodName = "PayrollPeriodName";
			 public const string SRPaySequent = "SRPaySequent";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string PayDate = "PayDate";
			 public const string SPTYear = "SPTYear";
			 public const string SPTMonth = "SPTMonth";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string WageProcessTypeID = "WageProcessTypeID";
			 public const string IsMoslemTHR = "IsMoslemTHR";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string PayrollPeriodCode = "PayrollPeriodCode";
			 public const string PayrollPeriodName = "PayrollPeriodName";
			 public const string SRPaySequent = "SRPaySequent";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string PayDate = "PayDate";
			 public const string SPTYear = "SPTYear";
			 public const string SPTMonth = "SPTMonth";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string WageProcessTypeID = "WageProcessTypeID";
			 public const string IsMoslemTHR = "IsMoslemTHR";
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
			lock (typeof(PayrollPeriodMetadata))
			{
				if(PayrollPeriodMetadata.mapDelegates == null)
				{
					PayrollPeriodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PayrollPeriodMetadata.meta == null)
				{
					PayrollPeriodMetadata.meta = new PayrollPeriodMetadata();
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
				

				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PayrollPeriodName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaySequent", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PayDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SPTYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SPTMonth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WageProcessTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsMoslemTHR", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "PayrollPeriod";
				meta.Destination = "PayrollPeriod";
				
				meta.spInsert = "proc_PayrollPeriodInsert";				
				meta.spUpdate = "proc_PayrollPeriodUpdate";		
				meta.spDelete = "proc_PayrollPeriodDelete";
				meta.spLoadAll = "proc_PayrollPeriodLoadAll";
				meta.spLoadByPrimaryKey = "proc_PayrollPeriodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PayrollPeriodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
