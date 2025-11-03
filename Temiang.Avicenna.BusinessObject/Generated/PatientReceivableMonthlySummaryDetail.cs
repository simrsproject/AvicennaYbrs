/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/16/2021 11:21:29 AM
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
	abstract public class esPatientReceivableMonthlySummaryDetailCollection : esEntityCollectionWAuditLog
	{
		public esPatientReceivableMonthlySummaryDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientReceivableMonthlySummaryDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientReceivableMonthlySummaryDetailQuery query)
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
			this.InitQuery(query as esPatientReceivableMonthlySummaryDetailQuery);
		}
		#endregion
			
		virtual public PatientReceivableMonthlySummaryDetail DetachEntity(PatientReceivableMonthlySummaryDetail entity)
		{
			return base.DetachEntity(entity) as PatientReceivableMonthlySummaryDetail;
		}
		
		virtual public PatientReceivableMonthlySummaryDetail AttachEntity(PatientReceivableMonthlySummaryDetail entity)
		{
			return base.AttachEntity(entity) as PatientReceivableMonthlySummaryDetail;
		}
		
		virtual public void Combine(PatientReceivableMonthlySummaryDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientReceivableMonthlySummaryDetail this[int index]
		{
			get
			{
				return base[index] as PatientReceivableMonthlySummaryDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientReceivableMonthlySummaryDetail);
		}
	}

	[Serializable]
	abstract public class esPatientReceivableMonthlySummaryDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientReceivableMonthlySummaryDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientReceivableMonthlySummaryDetail()
		{
		}
	
		public esPatientReceivableMonthlySummaryDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 detailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailID);
			else
				return LoadByPrimaryKeyStoredProcedure(detailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 detailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailID);
			else
				return LoadByPrimaryKeyStoredProcedure(detailID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 detailID)
		{
			esPatientReceivableMonthlySummaryDetailQuery query = this.GetDynamicQuery();
			query.Where(query.DetailID==detailID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 detailID)
		{
			esParameters parms = new esParameters();
			parms.Add("DetailID",detailID);
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
						case "DetailID": this.str.DetailID = (string)value; break;
						case "ID": this.str.ID = (string)value; break;
						case "SRBillingGroup": this.str.SRBillingGroup = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "IsRevenue": this.str.IsRevenue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DetailID":
						
							if (value == null || value is System.Int64)
								this.DetailID = (System.Int64?)value;
							break;
						case "ID":
						
							if (value == null || value is System.Int32)
								this.ID = (System.Int32?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsRevenue":
						
							if (value == null || value is System.Boolean)
								this.IsRevenue = (System.Boolean?)value;
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
		/// Maps to PatientReceivableMonthlySummaryDetail.DetailID
		/// </summary>
		virtual public System.Int64? DetailID
		{
			get
			{
				return base.GetSystemInt64(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.DetailID);
			}
			
			set
			{
				base.SetSystemInt64(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.DetailID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.ID
		/// </summary>
		virtual public System.Int32? ID
		{
			get
			{
				return base.GetSystemInt32(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ID);
			}
			
			set
			{
				base.SetSystemInt32(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.SRBillingGroup
		/// </summary>
		virtual public System.String SRBillingGroup
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SRBillingGroup);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SRBillingGroup, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummaryDetail.IsRevenue
		/// </summary>
		virtual public System.Boolean? IsRevenue
		{
			get
			{
				return base.GetSystemBoolean(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.IsRevenue);
			}
			
			set
			{
				base.SetSystemBoolean(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.IsRevenue, value);
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
			public esStrings(esPatientReceivableMonthlySummaryDetail entity)
			{
				this.entity = entity;
			}
			public System.String DetailID
			{
				get
				{
					System.Int64? data = entity.DetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailID = null;
					else entity.DetailID = Convert.ToInt64(value);
				}
			}
			public System.String ID
			{
				get
				{
					System.Int32? data = entity.ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ID = null;
					else entity.ID = Convert.ToInt32(value);
				}
			}
			public System.String SRBillingGroup
			{
				get
				{
					System.String data = entity.SRBillingGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBillingGroup = null;
					else entity.SRBillingGroup = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
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
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
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
			public System.String IsRevenue
			{
				get
				{
					System.Boolean? data = entity.IsRevenue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRevenue = null;
					else entity.IsRevenue = Convert.ToBoolean(value);
				}
			}
			private esPatientReceivableMonthlySummaryDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientReceivableMonthlySummaryDetailQuery query)
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
				throw new Exception("esPatientReceivableMonthlySummaryDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientReceivableMonthlySummaryDetail : esPatientReceivableMonthlySummaryDetail
	{	
	}

	[Serializable]
	abstract public class esPatientReceivableMonthlySummaryDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientReceivableMonthlySummaryDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem DetailID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.DetailID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRBillingGroup
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SRBillingGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsRevenue
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.IsRevenue, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientReceivableMonthlySummaryDetailCollection")]
	public partial class PatientReceivableMonthlySummaryDetailCollection : esPatientReceivableMonthlySummaryDetailCollection, IEnumerable< PatientReceivableMonthlySummaryDetail>
	{
		public PatientReceivableMonthlySummaryDetailCollection()
		{

		}	
		
		public static implicit operator List< PatientReceivableMonthlySummaryDetail>(PatientReceivableMonthlySummaryDetailCollection coll)
		{
			List< PatientReceivableMonthlySummaryDetail> list = new List< PatientReceivableMonthlySummaryDetail>();
			
			foreach (PatientReceivableMonthlySummaryDetail emp in coll)
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
				return  PatientReceivableMonthlySummaryDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientReceivableMonthlySummaryDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientReceivableMonthlySummaryDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientReceivableMonthlySummaryDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientReceivableMonthlySummaryDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientReceivableMonthlySummaryDetailQuery();
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
		public bool Load(PatientReceivableMonthlySummaryDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientReceivableMonthlySummaryDetail AddNew()
		{
			PatientReceivableMonthlySummaryDetail entity = base.AddNewEntity() as PatientReceivableMonthlySummaryDetail;
			
			return entity;		
		}
		public PatientReceivableMonthlySummaryDetail FindByPrimaryKey(Int64 detailID)
		{
			return base.FindByPrimaryKey(detailID) as PatientReceivableMonthlySummaryDetail;
		}

		#region IEnumerable< PatientReceivableMonthlySummaryDetail> Members

		IEnumerator< PatientReceivableMonthlySummaryDetail> IEnumerable< PatientReceivableMonthlySummaryDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientReceivableMonthlySummaryDetail;
			}
		}

		#endregion
		
		private PatientReceivableMonthlySummaryDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientReceivableMonthlySummaryDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientReceivableMonthlySummaryDetail ({DetailID})")]
	[Serializable]
	public partial class PatientReceivableMonthlySummaryDetail : esPatientReceivableMonthlySummaryDetail
	{
		public PatientReceivableMonthlySummaryDetail()
		{
		}	
	
		public PatientReceivableMonthlySummaryDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientReceivableMonthlySummaryDetailMetadata.Meta();
			}
		}	
	
		override protected esPatientReceivableMonthlySummaryDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientReceivableMonthlySummaryDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientReceivableMonthlySummaryDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientReceivableMonthlySummaryDetailQuery();
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
		public bool Load(PatientReceivableMonthlySummaryDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientReceivableMonthlySummaryDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientReceivableMonthlySummaryDetailQuery : esPatientReceivableMonthlySummaryDetailQuery
	{
		public PatientReceivableMonthlySummaryDetailQuery()
		{

		}		
		
		public PatientReceivableMonthlySummaryDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientReceivableMonthlySummaryDetailQuery";
        }
	}

	[Serializable]
	public partial class PatientReceivableMonthlySummaryDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientReceivableMonthlySummaryDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.DetailID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.DetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.ID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SRBillingGroup, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.SRBillingGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ChartOfAccountId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ClassID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ParamedicID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ServiceUnitID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.ItemID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.TransactionNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.SequenceNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryDetailMetadata.ColumnNames.IsRevenue, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientReceivableMonthlySummaryDetailMetadata.PropertyNames.IsRevenue;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientReceivableMonthlySummaryDetailMetadata Meta()
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
			public const string DetailID = "DetailID";
			public const string ID = "ID";
			public const string SRBillingGroup = "SRBillingGroup";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string Amount = "Amount";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string ClassID = "ClassID";
			public const string ParamedicID = "ParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string IsRevenue = "IsRevenue";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string DetailID = "DetailID";
			public const string ID = "ID";
			public const string SRBillingGroup = "SRBillingGroup";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string Amount = "Amount";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string ClassID = "ClassID";
			public const string ParamedicID = "ParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string IsRevenue = "IsRevenue";
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
			lock (typeof(PatientReceivableMonthlySummaryDetailMetadata))
			{
				if(PatientReceivableMonthlySummaryDetailMetadata.mapDelegates == null)
				{
					PatientReceivableMonthlySummaryDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientReceivableMonthlySummaryDetailMetadata.meta == null)
				{
					PatientReceivableMonthlySummaryDetailMetadata.meta = new PatientReceivableMonthlySummaryDetailMetadata();
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
				
				meta.AddTypeMap("DetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRBillingGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRevenue", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "PatientReceivableMonthlySummaryDetail";
				meta.Destination = "PatientReceivableMonthlySummaryDetail";
				meta.spInsert = "proc_PatientReceivableMonthlySummaryDetailInsert";				
				meta.spUpdate = "proc_PatientReceivableMonthlySummaryDetailUpdate";		
				meta.spDelete = "proc_PatientReceivableMonthlySummaryDetailDelete";
				meta.spLoadAll = "proc_PatientReceivableMonthlySummaryDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientReceivableMonthlySummaryDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientReceivableMonthlySummaryDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
