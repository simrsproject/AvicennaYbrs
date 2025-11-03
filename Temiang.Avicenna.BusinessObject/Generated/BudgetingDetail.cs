/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/23/2021 2:57:26 PM
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
	abstract public class esBudgetingDetailCollection : esEntityCollectionWAuditLog
	{
		public esBudgetingDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BudgetingDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBudgetingDetailQuery query)
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
			this.InitQuery(query as esBudgetingDetailQuery);
		}
		#endregion
			
		virtual public BudgetingDetail DetachEntity(BudgetingDetail entity)
		{
			return base.DetachEntity(entity) as BudgetingDetail;
		}
		
		virtual public BudgetingDetail AttachEntity(BudgetingDetail entity)
		{
			return base.AttachEntity(entity) as BudgetingDetail;
		}
		
		virtual public void Combine(BudgetingDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BudgetingDetail this[int index]
		{
			get
			{
				return base[index] as BudgetingDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BudgetingDetail);
		}
	}

	[Serializable]
	abstract public class esBudgetingDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBudgetingDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBudgetingDetail()
		{
		}
	
		public esBudgetingDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String budgetingNo, Int32 revision, Int32 chartOfAccountID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision, chartOfAccountID);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision, chartOfAccountID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String budgetingNo, Int32 revision, Int32 chartOfAccountID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision, chartOfAccountID);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision, chartOfAccountID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String budgetingNo, Int32 revision, Int32 chartOfAccountID)
		{
			esBudgetingDetailQuery query = this.GetDynamicQuery();
			query.Where(query.BudgetingNo==budgetingNo, query.Revision==revision, query.ChartOfAccountID==chartOfAccountID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String budgetingNo, Int32 revision, Int32 chartOfAccountID)
		{
			esParameters parms = new esParameters();
			parms.Add("BudgetingNo",budgetingNo);
			parms.Add("Revision",revision);
			parms.Add("ChartOfAccountID",chartOfAccountID);
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
		/// Maps to BudgetingDetail.BudgetingNo
		/// </summary>
		virtual public System.String BudgetingNo
		{
			get
			{
				return base.GetSystemString(BudgetingDetailMetadata.ColumnNames.BudgetingNo);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailMetadata.ColumnNames.BudgetingNo, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Revision
		/// </summary>
		virtual public System.Int32? Revision
		{
			get
			{
				return base.GetSystemInt32(BudgetingDetailMetadata.ColumnNames.Revision);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingDetailMetadata.ColumnNames.Revision, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.ChartOfAccountID
		/// </summary>
		virtual public System.Int32? ChartOfAccountID
		{
			get
			{
				return base.GetSystemInt32(BudgetingDetailMetadata.ColumnNames.ChartOfAccountID);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingDetailMetadata.ColumnNames.ChartOfAccountID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit01
		/// </summary>
		virtual public System.Decimal? Limit01
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit01);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit01, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit02
		/// </summary>
		virtual public System.Decimal? Limit02
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit02);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit02, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit03
		/// </summary>
		virtual public System.Decimal? Limit03
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit03);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit03, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit04
		/// </summary>
		virtual public System.Decimal? Limit04
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit04);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit04, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit05
		/// </summary>
		virtual public System.Decimal? Limit05
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit05);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit05, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit06
		/// </summary>
		virtual public System.Decimal? Limit06
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit06);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit06, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit07
		/// </summary>
		virtual public System.Decimal? Limit07
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit07);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit07, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit08
		/// </summary>
		virtual public System.Decimal? Limit08
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit08);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit08, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit09
		/// </summary>
		virtual public System.Decimal? Limit09
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit09);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit09, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit10
		/// </summary>
		virtual public System.Decimal? Limit10
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit10);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit10, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit11
		/// </summary>
		virtual public System.Decimal? Limit11
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit11);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit11, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.Limit12
		/// </summary>
		virtual public System.Decimal? Limit12
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit12);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingDetailMetadata.ColumnNames.Limit12, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingDetailMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingDetailMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esBudgetingDetail entity)
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
			private esBudgetingDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBudgetingDetailQuery query)
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
				throw new Exception("esBudgetingDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BudgetingDetail : esBudgetingDetail
	{	
	}

	[Serializable]
	abstract public class esBudgetingDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem BudgetingNo
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.BudgetingNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Revision
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Revision, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.ChartOfAccountID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Limit01
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit01, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit02
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit02, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit03
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit03, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit04
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit04, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit05
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit05, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit06
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit06, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit07
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit07, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit08
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit08, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit09
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit09, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit10
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit10, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit11
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit11, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Limit12
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.Limit12, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BudgetingDetailCollection")]
	public partial class BudgetingDetailCollection : esBudgetingDetailCollection, IEnumerable< BudgetingDetail>
	{
		public BudgetingDetailCollection()
		{

		}	
		
		public static implicit operator List< BudgetingDetail>(BudgetingDetailCollection coll)
		{
			List< BudgetingDetail> list = new List< BudgetingDetail>();
			
			foreach (BudgetingDetail emp in coll)
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
				return  BudgetingDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BudgetingDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BudgetingDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BudgetingDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingDetailQuery();
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
		public bool Load(BudgetingDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BudgetingDetail AddNew()
		{
			BudgetingDetail entity = base.AddNewEntity() as BudgetingDetail;
			
			return entity;		
		}
		public BudgetingDetail FindByPrimaryKey(String budgetingNo, Int32 revision, Int32 chartOfAccountID)
		{
			return base.FindByPrimaryKey(budgetingNo, revision, chartOfAccountID) as BudgetingDetail;
		}

		#region IEnumerable< BudgetingDetail> Members

		IEnumerator< BudgetingDetail> IEnumerable< BudgetingDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BudgetingDetail;
			}
		}

		#endregion
		
		private BudgetingDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BudgetingDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BudgetingDetail ({BudgetingNo, Revision, ChartOfAccountID})")]
	[Serializable]
	public partial class BudgetingDetail : esBudgetingDetail
	{
		public BudgetingDetail()
		{
		}	
	
		public BudgetingDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingDetailMetadata.Meta();
			}
		}	
	
		override protected esBudgetingDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BudgetingDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingDetailQuery();
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
		public bool Load(BudgetingDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BudgetingDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BudgetingDetailQuery : esBudgetingDetailQuery
	{
		public BudgetingDetailQuery()
		{

		}		
		
		public BudgetingDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BudgetingDetailQuery";
        }
	}

	[Serializable]
	public partial class BudgetingDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BudgetingDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.BudgetingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.BudgetingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Revision, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Revision;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.ChartOfAccountID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.ChartOfAccountID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit01, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit01;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit02, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit02;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit03, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit03;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit04, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit04;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit05, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit05;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit06, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit06;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit07, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit07;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit08, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit08;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit09, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit09;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit10, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit10;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit11, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit11;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.Limit12, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.Limit12;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.CreatedByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.CreatedDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingDetailMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BudgetingDetailMetadata Meta()
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
			lock (typeof(BudgetingDetailMetadata))
			{
				if(BudgetingDetailMetadata.mapDelegates == null)
				{
					BudgetingDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BudgetingDetailMetadata.meta == null)
				{
					BudgetingDetailMetadata.meta = new BudgetingDetailMetadata();
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
		

				meta.Source = "BudgetingDetail";
				meta.Destination = "BudgetingDetail";
				meta.spInsert = "proc_BudgetingDetailInsert";				
				meta.spUpdate = "proc_BudgetingDetailUpdate";		
				meta.spDelete = "proc_BudgetingDetailDelete";
				meta.spLoadAll = "proc_BudgetingDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_BudgetingDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BudgetingDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
