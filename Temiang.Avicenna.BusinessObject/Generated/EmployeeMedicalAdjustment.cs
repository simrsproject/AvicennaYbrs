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
	abstract public class esEmployeeMedicalAdjustmentCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeMedicalAdjustmentCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeMedicalAdjustmentCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeMedicalAdjustmentQuery query)
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
			this.InitQuery(query as esEmployeeMedicalAdjustmentQuery);
		}
		#endregion
		
		virtual public EmployeeMedicalAdjustment DetachEntity(EmployeeMedicalAdjustment entity)
		{
			return base.DetachEntity(entity) as EmployeeMedicalAdjustment;
		}
		
		virtual public EmployeeMedicalAdjustment AttachEntity(EmployeeMedicalAdjustment entity)
		{
			return base.AttachEntity(entity) as EmployeeMedicalAdjustment;
		}
		
		virtual public void Combine(EmployeeMedicalAdjustmentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeMedicalAdjustment this[int index]
		{
			get
			{
				return base[index] as EmployeeMedicalAdjustment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeMedicalAdjustment);
		}
	}



	[Serializable]
	abstract public class esEmployeeMedicalAdjustment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeMedicalAdjustmentQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeMedicalAdjustment()
		{

		}

		public esEmployeeMedicalAdjustment(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeMedicalAdjustmentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeMedicalAdjustmentID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeMedicalAdjustmentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeMedicalAdjustmentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeMedicalAdjustmentID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeMedicalAdjustmentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeMedicalAdjustmentID)
		{
			esEmployeeMedicalAdjustmentQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeMedicalAdjustmentID == employeeMedicalAdjustmentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeMedicalAdjustmentID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeMedicalAdjustmentID",employeeMedicalAdjustmentID);
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
						case "EmployeeMedicalAdjustmentID": this.str.EmployeeMedicalAdjustmentID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "YearPeriodID": this.str.YearPeriodID = (string)value; break;							
						case "MedicalBenefitInfoID": this.str.MedicalBenefitInfoID = (string)value; break;							
						case "AdjustmentDate": this.str.AdjustmentDate = (string)value; break;							
						case "AdjustmentAmount": this.str.AdjustmentAmount = (string)value; break;							
						case "DependentAdjustmentAmount": this.str.DependentAdjustmentAmount = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeMedicalAdjustmentID":
						
							if (value == null || value is System.Int32)
								this.EmployeeMedicalAdjustmentID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "YearPeriodID":
						
							if (value == null || value is System.Int32)
								this.YearPeriodID = (System.Int32?)value;
							break;
						
						case "MedicalBenefitInfoID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitInfoID = (System.Int32?)value;
							break;
						
						case "AdjustmentDate":
						
							if (value == null || value is System.DateTime)
								this.AdjustmentDate = (System.DateTime?)value;
							break;
						
						case "AdjustmentAmount":
						
							if (value == null || value is System.Decimal)
								this.AdjustmentAmount = (System.Decimal?)value;
							break;
						
						case "DependentAdjustmentAmount":
						
							if (value == null || value is System.Decimal)
								this.DependentAdjustmentAmount = (System.Decimal?)value;
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
		/// Maps to EmployeeMedicalAdjustment.EmployeeMedicalAdjustmentID
		/// </summary>
		virtual public System.Int32? EmployeeMedicalAdjustmentID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.EmployeeMedicalAdjustmentID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.EmployeeMedicalAdjustmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.YearPeriodID
		/// </summary>
		virtual public System.Int32? YearPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.YearPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.YearPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.MedicalBenefitInfoID
		/// </summary>
		virtual public System.Int32? MedicalBenefitInfoID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.MedicalBenefitInfoID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalAdjustmentMetadata.ColumnNames.MedicalBenefitInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.AdjustmentDate
		/// </summary>
		virtual public System.DateTime? AdjustmentDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.AdjustmentAmount
		/// </summary>
		virtual public System.Decimal? AdjustmentAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.DependentAdjustmentAmount
		/// </summary>
		virtual public System.Decimal? DependentAdjustmentAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalAdjustmentMetadata.ColumnNames.DependentAdjustmentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalAdjustmentMetadata.ColumnNames.DependentAdjustmentAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeMedicalAdjustmentMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeMedicalAdjustmentMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalAdjustment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeMedicalAdjustment entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeMedicalAdjustmentID
			{
				get
				{
					System.Int32? data = entity.EmployeeMedicalAdjustmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeMedicalAdjustmentID = null;
					else entity.EmployeeMedicalAdjustmentID = Convert.ToInt32(value);
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
				
			public System.String YearPeriodID
			{
				get
				{
					System.Int32? data = entity.YearPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriodID = null;
					else entity.YearPeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String MedicalBenefitInfoID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitInfoID = null;
					else entity.MedicalBenefitInfoID = Convert.ToInt32(value);
				}
			}
				
			public System.String AdjustmentDate
			{
				get
				{
					System.DateTime? data = entity.AdjustmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustmentDate = null;
					else entity.AdjustmentDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String AdjustmentAmount
			{
				get
				{
					System.Decimal? data = entity.AdjustmentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustmentAmount = null;
					else entity.AdjustmentAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DependentAdjustmentAmount
			{
				get
				{
					System.Decimal? data = entity.DependentAdjustmentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentAdjustmentAmount = null;
					else entity.DependentAdjustmentAmount = Convert.ToDecimal(value);
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
			

			private esEmployeeMedicalAdjustment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeMedicalAdjustmentQuery query)
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
				throw new Exception("esEmployeeMedicalAdjustment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeMedicalAdjustment : esEmployeeMedicalAdjustment
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
	abstract public class esEmployeeMedicalAdjustmentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMedicalAdjustmentMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeMedicalAdjustmentID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.EmployeeMedicalAdjustmentID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem YearPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.YearPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MedicalBenefitInfoID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.MedicalBenefitInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AdjustmentDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AdjustmentAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DependentAdjustmentAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.DependentAdjustmentAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeMedicalAdjustmentCollection")]
	public partial class EmployeeMedicalAdjustmentCollection : esEmployeeMedicalAdjustmentCollection, IEnumerable<EmployeeMedicalAdjustment>
	{
		public EmployeeMedicalAdjustmentCollection()
		{

		}
		
		public static implicit operator List<EmployeeMedicalAdjustment>(EmployeeMedicalAdjustmentCollection coll)
		{
			List<EmployeeMedicalAdjustment> list = new List<EmployeeMedicalAdjustment>();
			
			foreach (EmployeeMedicalAdjustment emp in coll)
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
				return  EmployeeMedicalAdjustmentMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMedicalAdjustmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeMedicalAdjustment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeMedicalAdjustment();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeMedicalAdjustmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMedicalAdjustmentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeMedicalAdjustmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeMedicalAdjustment AddNew()
		{
			EmployeeMedicalAdjustment entity = base.AddNewEntity() as EmployeeMedicalAdjustment;
			
			return entity;
		}

		public EmployeeMedicalAdjustment FindByPrimaryKey(System.Int32 employeeMedicalAdjustmentID)
		{
			return base.FindByPrimaryKey(employeeMedicalAdjustmentID) as EmployeeMedicalAdjustment;
		}


		#region IEnumerable<EmployeeMedicalAdjustment> Members

		IEnumerator<EmployeeMedicalAdjustment> IEnumerable<EmployeeMedicalAdjustment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeMedicalAdjustment;
			}
		}

		#endregion
		
		private EmployeeMedicalAdjustmentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeMedicalAdjustment' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeMedicalAdjustment ({EmployeeMedicalAdjustmentID})")]
	[Serializable]
	public partial class EmployeeMedicalAdjustment : esEmployeeMedicalAdjustment
	{
		public EmployeeMedicalAdjustment()
		{

		}
	
		public EmployeeMedicalAdjustment(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMedicalAdjustmentMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeMedicalAdjustmentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMedicalAdjustmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeMedicalAdjustmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMedicalAdjustmentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeMedicalAdjustmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeMedicalAdjustmentQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeMedicalAdjustmentQuery : esEmployeeMedicalAdjustmentQuery
	{
		public EmployeeMedicalAdjustmentQuery()
		{

		}		
		
		public EmployeeMedicalAdjustmentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeMedicalAdjustmentQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeMedicalAdjustmentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeMedicalAdjustmentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.EmployeeMedicalAdjustmentID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.EmployeeMedicalAdjustmentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.YearPeriodID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.YearPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.MedicalBenefitInfoID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.MedicalBenefitInfoID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.AdjustmentDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.AdjustmentAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.AdjustmentAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.DependentAdjustmentAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.DependentAdjustmentAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.IsApproved;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalAdjustmentMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalAdjustmentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeMedicalAdjustmentMetadata Meta()
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
			 public const string EmployeeMedicalAdjustmentID = "EmployeeMedicalAdjustmentID";
			 public const string PersonID = "PersonID";
			 public const string YearPeriodID = "YearPeriodID";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string AdjustmentDate = "AdjustmentDate";
			 public const string AdjustmentAmount = "AdjustmentAmount";
			 public const string DependentAdjustmentAmount = "DependentAdjustmentAmount";
			 public const string IsApproved = "IsApproved";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeMedicalAdjustmentID = "EmployeeMedicalAdjustmentID";
			 public const string PersonID = "PersonID";
			 public const string YearPeriodID = "YearPeriodID";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string AdjustmentDate = "AdjustmentDate";
			 public const string AdjustmentAmount = "AdjustmentAmount";
			 public const string DependentAdjustmentAmount = "DependentAdjustmentAmount";
			 public const string IsApproved = "IsApproved";
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
			lock (typeof(EmployeeMedicalAdjustmentMetadata))
			{
				if(EmployeeMedicalAdjustmentMetadata.mapDelegates == null)
				{
					EmployeeMedicalAdjustmentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeMedicalAdjustmentMetadata.meta == null)
				{
					EmployeeMedicalAdjustmentMetadata.meta = new EmployeeMedicalAdjustmentMetadata();
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
				

				meta.AddTypeMap("EmployeeMedicalAdjustmentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MedicalBenefitInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AdjustmentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AdjustmentAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DependentAdjustmentAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeMedicalAdjustment";
				meta.Destination = "EmployeeMedicalAdjustment";
				
				meta.spInsert = "proc_EmployeeMedicalAdjustmentInsert";				
				meta.spUpdate = "proc_EmployeeMedicalAdjustmentUpdate";		
				meta.spDelete = "proc_EmployeeMedicalAdjustmentDelete";
				meta.spLoadAll = "proc_EmployeeMedicalAdjustmentLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeMedicalAdjustmentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeMedicalAdjustmentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
