/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/15/2021 7:46:38 AM
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
	abstract public class esBudgetingDetailItemGroupCollection : esEntityCollectionWAuditLog
	{
		public esBudgetingDetailItemGroupCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BudgetingDetailItemGroupCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBudgetingDetailItemGroupQuery query)
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
			this.InitQuery(query as esBudgetingDetailItemGroupQuery);
		}
		#endregion
			
		virtual public BudgetingDetailItemGroup DetachEntity(BudgetingDetailItemGroup entity)
		{
			return base.DetachEntity(entity) as BudgetingDetailItemGroup;
		}
		
		virtual public BudgetingDetailItemGroup AttachEntity(BudgetingDetailItemGroup entity)
		{
			return base.AttachEntity(entity) as BudgetingDetailItemGroup;
		}
		
		virtual public void Combine(BudgetingDetailItemGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BudgetingDetailItemGroup this[int index]
		{
			get
			{
				return base[index] as BudgetingDetailItemGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BudgetingDetailItemGroup);
		}
	}

	[Serializable]
	abstract public class esBudgetingDetailItemGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBudgetingDetailItemGroupQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBudgetingDetailItemGroup()
		{
		}
	
		public esBudgetingDetailItemGroup(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision, chartOfAccountID, itemGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision, chartOfAccountID, itemGroupID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision, chartOfAccountID, itemGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision, chartOfAccountID, itemGroupID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemGroupID)
		{
			esBudgetingDetailItemGroupQuery query = this.GetDynamicQuery();
			query.Where(query.BudgetingNo==budgetingNo, query.Revision==revision, query.ChartOfAccountID==chartOfAccountID, query.ItemGroupID==itemGroupID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("BudgetingNo",budgetingNo);
			parms.Add("Revision",revision);
			parms.Add("ChartOfAccountID",chartOfAccountID);
			parms.Add("ItemGroupID",itemGroupID);
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
						case "BudgetingNo": this.str.BudgetingNo = (string)value; break;
						case "Revision": this.str.Revision = (string)value; break;
						case "ChartOfAccountID": this.str.ChartOfAccountID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "Limit01": this.str.Limit01 = (string)value; break;
						case "Limit02": this.str.Limit02 = (string)value; break;
						case "Limit03": this.str.Limit03 = (string)value; break;
						case "Limit04": this.str.Limit04 = (string)value; break;
						case "Limit05": this.str.Limit05 = (string)value; break;
						case "Limit06": this.str.Limit06 = (string)value; break;
						case "Limit07": this.str.Limit07 = (string)value; break;
						case "Limit08": this.str.Limit08 = (string)value; break;
						case "Limit09": this.str.Limit09 = (string)value; break;
						case "Limit10": this.str.Limit10 = (string)value; break;
						case "Limit11": this.str.Limit11 = (string)value; break;
						case "Limit12": this.str.Limit12 = (string)value; break;
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
						case "Revision":
						
							if (value == null || value is System.Int32)
								this.Revision = (System.Int32?)value;
							break;
						case "ChartOfAccountID":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountID = (System.Int32?)value;
							break;
						case "Limit01":
						
							if (value == null || value is System.Decimal)
								this.Limit01 = (System.Decimal?)value;
							break;
						case "Limit02":
						
							if (value == null || value is System.Decimal)
								this.Limit02 = (System.Decimal?)value;
							break;
						case "Limit03":
						
							if (value == null || value is System.Decimal)
								this.Limit03 = (System.Decimal?)value;
							break;
						case "Limit04":
						
							if (value == null || value is System.Decimal)
								this.Limit04 = (System.Decimal?)value;
							break;
						case "Limit05":
						
							if (value == null || value is System.Decimal)
								this.Limit05 = (System.Decimal?)value;
							break;
						case "Limit06":
						
							if (value == null || value is System.Decimal)
								this.Limit06 = (System.Decimal?)value;
							break;
						case "Limit07":
						
							if (value == null || value is System.Decimal)
								this.Limit07 = (System.Decimal?)value;
							break;
						case "Limit08":
						
							if (value == null || value is System.Decimal)
								this.Limit08 = (System.Decimal?)value;
							break;
						case "Limit09":
						
							if (value == null || value is System.Decimal)
								this.Limit09 = (System.Decimal?)value;
							break;
						case "Limit10":
						
							if (value == null || value is System.Decimal)
								this.Limit10 = (System.Decimal?)value;
							break;
						case "Limit11":
						
							if (value == null || value is System.Decimal)
								this.Limit11 = (System.Decimal?)value;
							break;
						case "Limit12":
						
							if (value == null || value is System.Decimal)
								this.Limit12 = (System.Decimal?)value;
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
		/// Maps to BudgetingDetailItemGroup.BudgetingNo
		/// </summary>
		virtual public System.String BudgetingNo
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.BudgetingNo);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.BudgetingNo, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Revision
		/// </summary>
		virtual public System.Int32? Revision
		{
			get
			{
				return base.GetSystemInt32(BudgetingDetailItemGroupMetadata.ColumnNames.Revision);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingDetailItemGroupMetadata.ColumnNames.Revision, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.ChartOfAccountID
		/// </summary>
		virtual public System.Int32? ChartOfAccountID
		{
			get
			{
				return base.GetSystemInt32(BudgetingDetailItemGroupMetadata.ColumnNames.ChartOfAccountID);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingDetailItemGroupMetadata.ColumnNames.ChartOfAccountID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit01
		/// </summary>
		virtual public System.Decimal? Limit01
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit01);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit01, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit02
		/// </summary>
		virtual public System.Decimal? Limit02
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit02);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit02, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit03
		/// </summary>
		virtual public System.Decimal? Limit03
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit03);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit03, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit04
		/// </summary>
		virtual public System.Decimal? Limit04
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit04);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit04, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit05
		/// </summary>
		virtual public System.Decimal? Limit05
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit05);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit05, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit06
		/// </summary>
		virtual public System.Decimal? Limit06
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit06);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit06, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit07
		/// </summary>
		virtual public System.Decimal? Limit07
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit07);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit07, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit08
		/// </summary>
		virtual public System.Decimal? Limit08
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit08);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit08, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit09
		/// </summary>
		virtual public System.Decimal? Limit09
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit09);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit09, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit10
		/// </summary>
		virtual public System.Decimal? Limit10
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit10);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit10, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit11
		/// </summary>
		virtual public System.Decimal? Limit11
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit11);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit11, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.Limit12
		/// </summary>
		virtual public System.Decimal? Limit12
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit12);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailItemGroupMetadata.ColumnNames.Limit12, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailItemGroupMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingDetailItemGroupMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItemGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esBudgetingDetailItemGroup entity)
			{
				this.entity = entity;
			}
			public System.String BudgetingNo
			{
				get
				{
					System.String data = entity.BudgetingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetingNo = null;
					else entity.BudgetingNo = Convert.ToString(value);
				}
			}
			public System.String Revision
			{
				get
				{
					System.Int32? data = entity.Revision;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Revision = null;
					else entity.Revision = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountID
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountID = null;
					else entity.ChartOfAccountID = Convert.ToInt32(value);
				}
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String Limit01
			{
				get
				{
					System.Decimal? data = entity.Limit01;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit01 = null;
					else entity.Limit01 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit02
			{
				get
				{
					System.Decimal? data = entity.Limit02;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit02 = null;
					else entity.Limit02 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit03
			{
				get
				{
					System.Decimal? data = entity.Limit03;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit03 = null;
					else entity.Limit03 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit04
			{
				get
				{
					System.Decimal? data = entity.Limit04;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit04 = null;
					else entity.Limit04 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit05
			{
				get
				{
					System.Decimal? data = entity.Limit05;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit05 = null;
					else entity.Limit05 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit06
			{
				get
				{
					System.Decimal? data = entity.Limit06;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit06 = null;
					else entity.Limit06 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit07
			{
				get
				{
					System.Decimal? data = entity.Limit07;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit07 = null;
					else entity.Limit07 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit08
			{
				get
				{
					System.Decimal? data = entity.Limit08;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit08 = null;
					else entity.Limit08 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit09
			{
				get
				{
					System.Decimal? data = entity.Limit09;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit09 = null;
					else entity.Limit09 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit10
			{
				get
				{
					System.Decimal? data = entity.Limit10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit10 = null;
					else entity.Limit10 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit11
			{
				get
				{
					System.Decimal? data = entity.Limit11;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit11 = null;
					else entity.Limit11 = Convert.ToDecimal(value);
				}
			}
			public System.String Limit12
			{
				get
				{
					System.Decimal? data = entity.Limit12;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Limit12 = null;
					else entity.Limit12 = Convert.ToDecimal(value);
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
			private esBudgetingDetailItemGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBudgetingDetailItemGroupQuery query)
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
				throw new Exception("esBudgetingDetailItemGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BudgetingDetailItemGroup : esBudgetingDetailItemGroup
	{	
	}

	[Serializable]
	abstract public class esBudgetingDetailItemGroupQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingDetailItemGroupMetadata.Meta();
			}
		}	
			
		public esQueryItem BudgetingNo
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.BudgetingNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Revision
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Revision, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.ChartOfAccountID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem Limit01
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit01, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit02
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit02, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit03
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit03, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit04
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit04, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit05
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit05, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit06
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit06, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit07
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit07, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit08
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit08, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit09
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit09, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit10
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit10, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit11
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit11, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit12
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.Limit12, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BudgetingDetailItemGroupCollection")]
	public partial class BudgetingDetailItemGroupCollection : esBudgetingDetailItemGroupCollection, IEnumerable< BudgetingDetailItemGroup>
	{
		public BudgetingDetailItemGroupCollection()
		{

		}	
		
		public static implicit operator List< BudgetingDetailItemGroup>(BudgetingDetailItemGroupCollection coll)
		{
			List< BudgetingDetailItemGroup> list = new List< BudgetingDetailItemGroup>();
			
			foreach (BudgetingDetailItemGroup emp in coll)
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
				return  BudgetingDetailItemGroupMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingDetailItemGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BudgetingDetailItemGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BudgetingDetailItemGroup();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BudgetingDetailItemGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingDetailItemGroupQuery();
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
		public bool Load(BudgetingDetailItemGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BudgetingDetailItemGroup AddNew()
		{
			BudgetingDetailItemGroup entity = base.AddNewEntity() as BudgetingDetailItemGroup;
			
			return entity;		
		}
		public BudgetingDetailItemGroup FindByPrimaryKey(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemGroupID)
		{
			return base.FindByPrimaryKey(budgetingNo, revision, chartOfAccountID, itemGroupID) as BudgetingDetailItemGroup;
		}

		#region IEnumerable< BudgetingDetailItemGroup> Members

		IEnumerator< BudgetingDetailItemGroup> IEnumerable< BudgetingDetailItemGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BudgetingDetailItemGroup;
			}
		}

		#endregion
		
		private BudgetingDetailItemGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BudgetingDetailItemGroup' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BudgetingDetailItemGroup ({BudgetingNo, Revision, ChartOfAccountID, ItemGroupID})")]
	[Serializable]
	public partial class BudgetingDetailItemGroup : esBudgetingDetailItemGroup
	{
		public BudgetingDetailItemGroup()
		{
		}	
	
		public BudgetingDetailItemGroup(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingDetailItemGroupMetadata.Meta();
			}
		}	
	
		override protected esBudgetingDetailItemGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingDetailItemGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BudgetingDetailItemGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingDetailItemGroupQuery();
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
		public bool Load(BudgetingDetailItemGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BudgetingDetailItemGroupQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BudgetingDetailItemGroupQuery : esBudgetingDetailItemGroupQuery
	{
		public BudgetingDetailItemGroupQuery()
		{

		}		
		
		public BudgetingDetailItemGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BudgetingDetailItemGroupQuery";
        }
	}

	[Serializable]
	public partial class BudgetingDetailItemGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BudgetingDetailItemGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.BudgetingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.BudgetingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Revision, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Revision;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.ChartOfAccountID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.ChartOfAccountID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.ItemGroupID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.ItemGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit01, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit01;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit02, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit02;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit03, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit03;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit04, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit04;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit05, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit05;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit06, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit06;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit07, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit07;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit08, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit08;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit09, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit09;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit10, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit10;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit11, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit11;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.Limit12, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.Limit12;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.CreatedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.CreatedDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailItemGroupMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailItemGroupMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BudgetingDetailItemGroupMetadata Meta()
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
			public const string BudgetingNo = "BudgetingNo";
			public const string Revision = "Revision";
			public const string ChartOfAccountID = "ChartOfAccountID";
			public const string ItemGroupID = "ItemGroupID";
			public const string Limit01 = "Limit01";
			public const string Limit02 = "Limit02";
			public const string Limit03 = "Limit03";
			public const string Limit04 = "Limit04";
			public const string Limit05 = "Limit05";
			public const string Limit06 = "Limit06";
			public const string Limit07 = "Limit07";
			public const string Limit08 = "Limit08";
			public const string Limit09 = "Limit09";
			public const string Limit10 = "Limit10";
			public const string Limit11 = "Limit11";
			public const string Limit12 = "Limit12";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BudgetingNo = "BudgetingNo";
			public const string Revision = "Revision";
			public const string ChartOfAccountID = "ChartOfAccountID";
			public const string ItemGroupID = "ItemGroupID";
			public const string Limit01 = "Limit01";
			public const string Limit02 = "Limit02";
			public const string Limit03 = "Limit03";
			public const string Limit04 = "Limit04";
			public const string Limit05 = "Limit05";
			public const string Limit06 = "Limit06";
			public const string Limit07 = "Limit07";
			public const string Limit08 = "Limit08";
			public const string Limit09 = "Limit09";
			public const string Limit10 = "Limit10";
			public const string Limit11 = "Limit11";
			public const string Limit12 = "Limit12";
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
			lock (typeof(BudgetingDetailItemGroupMetadata))
			{
				if(BudgetingDetailItemGroupMetadata.mapDelegates == null)
				{
					BudgetingDetailItemGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BudgetingDetailItemGroupMetadata.meta == null)
				{
					BudgetingDetailItemGroupMetadata.meta = new BudgetingDetailItemGroupMetadata();
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
				
				meta.AddTypeMap("BudgetingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Revision", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Limit01", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit02", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit03", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit04", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit05", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit06", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit07", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit08", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit09", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit10", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit11", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Limit12", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "BudgetingDetailItemGroup";
				meta.Destination = "BudgetingDetailItemGroup";
				meta.spInsert = "proc_BudgetingDetailItemGroupInsert";				
				meta.spUpdate = "proc_BudgetingDetailItemGroupUpdate";		
				meta.spDelete = "proc_BudgetingDetailItemGroupDelete";
				meta.spLoadAll = "proc_BudgetingDetailItemGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_BudgetingDetailItemGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BudgetingDetailItemGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
