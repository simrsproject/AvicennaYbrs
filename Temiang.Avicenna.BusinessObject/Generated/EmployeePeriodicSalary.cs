/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/29/2016 6:01:07 AM
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
	abstract public class esEmployeePeriodicSalaryCollection : esEntityCollectionWAuditLog
	{
		public esEmployeePeriodicSalaryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeePeriodicSalaryCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeePeriodicSalaryQuery query)
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
			this.InitQuery(query as esEmployeePeriodicSalaryQuery);
		}
		#endregion
		
		virtual public EmployeePeriodicSalary DetachEntity(EmployeePeriodicSalary entity)
		{
			return base.DetachEntity(entity) as EmployeePeriodicSalary;
		}
		
		virtual public EmployeePeriodicSalary AttachEntity(EmployeePeriodicSalary entity)
		{
			return base.AttachEntity(entity) as EmployeePeriodicSalary;
		}
		
		virtual public void Combine(EmployeePeriodicSalaryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeePeriodicSalary this[int index]
		{
			get
			{
				return base[index] as EmployeePeriodicSalary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeePeriodicSalary);
		}
	}



	[Serializable]
	abstract public class esEmployeePeriodicSalary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeePeriodicSalaryQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeePeriodicSalary()
		{

		}

		public esEmployeePeriodicSalary(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeePeriodicSalaryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeePeriodicSalaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeePeriodicSalaryID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeePeriodicSalaryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeePeriodicSalaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeePeriodicSalaryID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeePeriodicSalaryID)
		{
			esEmployeePeriodicSalaryQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeePeriodicSalaryID == employeePeriodicSalaryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeePeriodicSalaryID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeePeriodicSalaryID",employeePeriodicSalaryID);
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
						case "EmployeePeriodicSalaryID": this.str.EmployeePeriodicSalaryID = (string)value; break;							
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;							
						case "SRProcessType": this.str.SRProcessType = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "FromBasicSalaryAmount": this.str.FromBasicSalaryAmount = (string)value; break;							
						case "FromPositionGradeID": this.str.FromPositionGradeID = (string)value; break;							
						case "ToPositionGradeID": this.str.ToPositionGradeID = (string)value; break;							
						case "FromGradeYear": this.str.FromGradeYear = (string)value; break;							
						case "ToGradeYear": this.str.ToGradeYear = (string)value; break;							
						case "OvertimeAmount": this.str.OvertimeAmount = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeePeriodicSalaryID":
						
							if (value == null || value is System.Int32)
								this.EmployeePeriodicSalaryID = (System.Int32?)value;
							break;
						
						case "PayrollPeriodID":
						
							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "SalaryComponentID":
						
							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "FromBasicSalaryAmount":
						
							if (value == null || value is System.Decimal)
								this.FromBasicSalaryAmount = (System.Decimal?)value;
							break;
						
						case "FromPositionGradeID":
						
							if (value == null || value is System.Int32)
								this.FromPositionGradeID = (System.Int32?)value;
							break;
						
						case "ToPositionGradeID":
						
							if (value == null || value is System.Int32)
								this.ToPositionGradeID = (System.Int32?)value;
							break;
						
						case "FromGradeYear":
						
							if (value == null || value is System.Int32)
								this.FromGradeYear = (System.Int32?)value;
							break;
						
						case "ToGradeYear":
						
							if (value == null || value is System.Int32)
								this.ToGradeYear = (System.Int32?)value;
							break;
						
						case "OvertimeAmount":
						
							if (value == null || value is System.Decimal)
								this.OvertimeAmount = (System.Decimal?)value;
							break;
						
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
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
		/// Maps to EmployeePeriodicSalary.EmployeePeriodicSalaryID
		/// </summary>
		virtual public System.Int32? EmployeePeriodicSalaryID
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.EmployeePeriodicSalaryID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.EmployeePeriodicSalaryID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.PayrollPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.SalaryComponentID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.SRProcessType
		/// </summary>
		virtual public System.String SRProcessType
		{
			get
			{
				return base.GetSystemString(EmployeePeriodicSalaryMetadata.ColumnNames.SRProcessType);
			}
			
			set
			{
				base.SetSystemString(EmployeePeriodicSalaryMetadata.ColumnNames.SRProcessType, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(EmployeePeriodicSalaryMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeePeriodicSalaryMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.FromBasicSalaryAmount
		/// </summary>
		virtual public System.Decimal? FromBasicSalaryAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeePeriodicSalaryMetadata.ColumnNames.FromBasicSalaryAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeePeriodicSalaryMetadata.ColumnNames.FromBasicSalaryAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.FromPositionGradeID
		/// </summary>
		virtual public System.Int32? FromPositionGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.FromPositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.FromPositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.ToPositionGradeID
		/// </summary>
		virtual public System.Int32? ToPositionGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.ToPositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.ToPositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.FromGradeYear
		/// </summary>
		virtual public System.Int32? FromGradeYear
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.FromGradeYear);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.FromGradeYear, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.ToGradeYear
		/// </summary>
		virtual public System.Int32? ToGradeYear
		{
			get
			{
				return base.GetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.ToGradeYear);
			}
			
			set
			{
				base.SetSystemInt32(EmployeePeriodicSalaryMetadata.ColumnNames.ToGradeYear, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.OvertimeAmount
		/// </summary>
		virtual public System.Decimal? OvertimeAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeePeriodicSalaryMetadata.ColumnNames.OvertimeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeePeriodicSalaryMetadata.ColumnNames.OvertimeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeePeriodicSalary.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeePeriodicSalaryMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeePeriodicSalaryMetadata.ColumnNames.TransactionDate, value);
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
			public esStrings(esEmployeePeriodicSalary entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeePeriodicSalaryID
			{
				get
				{
					System.Int32? data = entity.EmployeePeriodicSalaryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeePeriodicSalaryID = null;
					else entity.EmployeePeriodicSalaryID = Convert.ToInt32(value);
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
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRProcessType
			{
				get
				{
					System.String data = entity.SRProcessType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcessType = null;
					else entity.SRProcessType = Convert.ToString(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
				
			public System.String FromBasicSalaryAmount
			{
				get
				{
					System.Decimal? data = entity.FromBasicSalaryAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromBasicSalaryAmount = null;
					else entity.FromBasicSalaryAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String FromPositionGradeID
			{
				get
				{
					System.Int32? data = entity.FromPositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromPositionGradeID = null;
					else entity.FromPositionGradeID = Convert.ToInt32(value);
				}
			}
				
			public System.String ToPositionGradeID
			{
				get
				{
					System.Int32? data = entity.ToPositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToPositionGradeID = null;
					else entity.ToPositionGradeID = Convert.ToInt32(value);
				}
			}
				
			public System.String FromGradeYear
			{
				get
				{
					System.Int32? data = entity.FromGradeYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromGradeYear = null;
					else entity.FromGradeYear = Convert.ToInt32(value);
				}
			}
				
			public System.String ToGradeYear
			{
				get
				{
					System.Int32? data = entity.ToGradeYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToGradeYear = null;
					else entity.ToGradeYear = Convert.ToInt32(value);
				}
			}
				
			public System.String OvertimeAmount
			{
				get
				{
					System.Decimal? data = entity.OvertimeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OvertimeAmount = null;
					else entity.OvertimeAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			

			private esEmployeePeriodicSalary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeePeriodicSalaryQuery query)
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
				throw new Exception("esEmployeePeriodicSalary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeePeriodicSalary : esEmployeePeriodicSalary
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
	abstract public class esEmployeePeriodicSalaryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeePeriodicSalaryMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeePeriodicSalaryID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.EmployeePeriodicSalaryID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRProcessType
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.SRProcessType, esSystemType.String);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem FromBasicSalaryAmount
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.FromBasicSalaryAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FromPositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.FromPositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ToPositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.ToPositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem FromGradeYear
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.FromGradeYear, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ToGradeYear
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.ToGradeYear, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OvertimeAmount
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.OvertimeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, EmployeePeriodicSalaryMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeePeriodicSalaryCollection")]
	public partial class EmployeePeriodicSalaryCollection : esEmployeePeriodicSalaryCollection, IEnumerable<EmployeePeriodicSalary>
	{
		public EmployeePeriodicSalaryCollection()
		{

		}
		
		public static implicit operator List<EmployeePeriodicSalary>(EmployeePeriodicSalaryCollection coll)
		{
			List<EmployeePeriodicSalary> list = new List<EmployeePeriodicSalary>();
			
			foreach (EmployeePeriodicSalary emp in coll)
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
				return  EmployeePeriodicSalaryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePeriodicSalaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeePeriodicSalary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeePeriodicSalary();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeePeriodicSalaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePeriodicSalaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeePeriodicSalaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeePeriodicSalary AddNew()
		{
			EmployeePeriodicSalary entity = base.AddNewEntity() as EmployeePeriodicSalary;
			
			return entity;
		}

		public EmployeePeriodicSalary FindByPrimaryKey(System.Int32 employeePeriodicSalaryID)
		{
			return base.FindByPrimaryKey(employeePeriodicSalaryID) as EmployeePeriodicSalary;
		}


		#region IEnumerable<EmployeePeriodicSalary> Members

		IEnumerator<EmployeePeriodicSalary> IEnumerable<EmployeePeriodicSalary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeePeriodicSalary;
			}
		}

		#endregion
		
		private EmployeePeriodicSalaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeePeriodicSalary' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeePeriodicSalary ({EmployeePeriodicSalaryID})")]
	[Serializable]
	public partial class EmployeePeriodicSalary : esEmployeePeriodicSalary
	{
		public EmployeePeriodicSalary()
		{

		}
	
		public EmployeePeriodicSalary(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeePeriodicSalaryMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeePeriodicSalaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePeriodicSalaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeePeriodicSalaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePeriodicSalaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeePeriodicSalaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeePeriodicSalaryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeePeriodicSalaryQuery : esEmployeePeriodicSalaryQuery
	{
		public EmployeePeriodicSalaryQuery()
		{

		}		
		
		public EmployeePeriodicSalaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeePeriodicSalaryQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeePeriodicSalaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeePeriodicSalaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.EmployeePeriodicSalaryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.EmployeePeriodicSalaryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.PayrollPeriodID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.SalaryComponentID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.SalaryComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.SRProcessType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.SRProcessType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.Amount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.Amount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.FromBasicSalaryAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.FromBasicSalaryAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.FromPositionGradeID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.FromPositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.ToPositionGradeID, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.ToPositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.FromGradeYear, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.FromGradeYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.ToGradeYear, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.ToGradeYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.OvertimeAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.OvertimeAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeePeriodicSalaryMetadata.ColumnNames.TransactionDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePeriodicSalaryMetadata.PropertyNames.TransactionDate;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeePeriodicSalaryMetadata Meta()
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
			 public const string EmployeePeriodicSalaryID = "EmployeePeriodicSalaryID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string PersonID = "PersonID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string SRProcessType = "SRProcessType";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string FromBasicSalaryAmount = "FromBasicSalaryAmount";
			 public const string FromPositionGradeID = "FromPositionGradeID";
			 public const string ToPositionGradeID = "ToPositionGradeID";
			 public const string FromGradeYear = "FromGradeYear";
			 public const string ToGradeYear = "ToGradeYear";
			 public const string OvertimeAmount = "OvertimeAmount";
			 public const string TransactionDate = "TransactionDate";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeePeriodicSalaryID = "EmployeePeriodicSalaryID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string PersonID = "PersonID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string SRProcessType = "SRProcessType";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string FromBasicSalaryAmount = "FromBasicSalaryAmount";
			 public const string FromPositionGradeID = "FromPositionGradeID";
			 public const string ToPositionGradeID = "ToPositionGradeID";
			 public const string FromGradeYear = "FromGradeYear";
			 public const string ToGradeYear = "ToGradeYear";
			 public const string OvertimeAmount = "OvertimeAmount";
			 public const string TransactionDate = "TransactionDate";
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
			lock (typeof(EmployeePeriodicSalaryMetadata))
			{
				if(EmployeePeriodicSalaryMetadata.mapDelegates == null)
				{
					EmployeePeriodicSalaryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeePeriodicSalaryMetadata.meta == null)
				{
					EmployeePeriodicSalaryMetadata.meta = new EmployeePeriodicSalaryMetadata();
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
				

				meta.AddTypeMap("EmployeePeriodicSalaryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRProcessType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromBasicSalaryAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FromPositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ToPositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FromGradeYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ToGradeYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OvertimeAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("smalldatetime", "System.DateTime"));			
				
				
				
				meta.Source = "EmployeePeriodicSalary";
				meta.Destination = "EmployeePeriodicSalary";
				
				meta.spInsert = "proc_EmployeePeriodicSalaryInsert";				
				meta.spUpdate = "proc_EmployeePeriodicSalaryUpdate";		
				meta.spDelete = "proc_EmployeePeriodicSalaryDelete";
				meta.spLoadAll = "proc_EmployeePeriodicSalaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeePeriodicSalaryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeePeriodicSalaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
