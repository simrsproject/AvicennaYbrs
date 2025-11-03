/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/4/2023 3:57:39 PM
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
	abstract public class esJournalTransactionDetailsCollection : esEntityCollectionWAuditLog
	{
		public esJournalTransactionDetailsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "JournalTransactionDetailsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esJournalTransactionDetailsQuery query)
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
			this.InitQuery(query as esJournalTransactionDetailsQuery);
		}
		#endregion
			
		virtual public JournalTransactionDetails DetachEntity(JournalTransactionDetails entity)
		{
			return base.DetachEntity(entity) as JournalTransactionDetails;
		}
		
		virtual public JournalTransactionDetails AttachEntity(JournalTransactionDetails entity)
		{
			return base.AttachEntity(entity) as JournalTransactionDetails;
		}
		
		virtual public void Combine(JournalTransactionDetailsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalTransactionDetails this[int index]
		{
			get
			{
				return base[index] as JournalTransactionDetails;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalTransactionDetails);
		}
	}

	[Serializable]
	abstract public class esJournalTransactionDetails : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalTransactionDetailsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esJournalTransactionDetails()
		{
		}
	
		public esJournalTransactionDetails(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 detailId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailId);
			else
				return LoadByPrimaryKeyStoredProcedure(detailId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 detailId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailId);
			else
				return LoadByPrimaryKeyStoredProcedure(detailId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 detailId)
		{
			esJournalTransactionDetailsQuery query = this.GetDynamicQuery();
			query.Where(query.DetailId==detailId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 detailId)
		{
			esParameters parms = new esParameters();
			parms.Add("DetailId",detailId);
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
						case "DetailId": this.str.DetailId = (string)value; break;
						case "JournalId": this.str.JournalId = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "Debit": this.str.Debit = (string)value; break;
						case "Credit": this.str.Credit = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "DocumentNumber": this.str.DocumentNumber = (string)value; break;
						case "DocumentPageNo": this.str.DocumentPageNo = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "SupplierID": this.str.SupplierID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "DueDate": this.str.DueDate = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "DocumentNumberSequenceNo": this.str.DocumentNumberSequenceNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "DateCreated": this.str.DateCreated = (string)value; break;
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsProrataDiscToRevenue": this.str.IsProrataDiscToRevenue = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DetailId":
						
							if (value == null || value is System.Int32)
								this.DetailId = (System.Int32?)value;
							break;
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
							break;
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "DocumentPageNo":
						
							if (value == null || value is System.Int32)
								this.DocumentPageNo = (System.Int32?)value;
							break;
						case "DueDate":
						
							if (value == null || value is System.DateTime)
								this.DueDate = (System.DateTime?)value;
							break;
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsProrataDiscToRevenue":
						
							if (value == null || value is System.Boolean)
								this.IsProrataDiscToRevenue = (System.Boolean?)value;
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
		/// Maps to JournalTransactionDetails.DetailId
		/// </summary>
		virtual public System.Int32? DetailId
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.DetailId);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.DetailId, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.JournalId, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(JournalTransactionDetailsMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(JournalTransactionDetailsMetadata.ColumnNames.Debit, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(JournalTransactionDetailsMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(JournalTransactionDetailsMetadata.ColumnNames.Credit, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.DocumentNumber
		/// </summary>
		virtual public System.String DocumentNumber
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.DocumentNumber);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.DocumentNumber, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.DocumentPageNo
		/// </summary>
		virtual public System.Int32? DocumentPageNo
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.DocumentPageNo);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionDetailsMetadata.ColumnNames.DocumentPageNo, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.SupplierID
		/// </summary>
		virtual public System.String SupplierID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.SupplierID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.SupplierID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.DueDate
		/// </summary>
		virtual public System.DateTime? DueDate
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionDetailsMetadata.ColumnNames.DueDate);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionDetailsMetadata.ColumnNames.DueDate, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.DocumentNumberSequenceNo
		/// </summary>
		virtual public System.String DocumentNumberSequenceNo
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.DocumentNumberSequenceNo);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.DocumentNumberSequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionDetailsMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionDetailsMetadata.ColumnNames.DateCreated, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.CreatedBy, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.IsProrataDiscToRevenue
		/// </summary>
		virtual public System.Boolean? IsProrataDiscToRevenue
		{
			get
			{
				return base.GetSystemBoolean(JournalTransactionDetailsMetadata.ColumnNames.IsProrataDiscToRevenue);
			}
			
			set
			{
				base.SetSystemBoolean(JournalTransactionDetailsMetadata.ColumnNames.IsProrataDiscToRevenue, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactionDetails.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(JournalTransactionDetailsMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionDetailsMetadata.ColumnNames.TariffComponentID, value);
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
			public esStrings(esJournalTransactionDetails entity)
			{
				this.entity = entity;
			}
			public System.String DetailId
			{
				get
				{
					System.Int32? data = entity.DetailId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailId = null;
					else entity.DetailId = Convert.ToInt32(value);
				}
			}
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
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
			public System.String Debit
			{
				get
				{
					System.Decimal? data = entity.Debit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Debit = null;
					else entity.Debit = Convert.ToDecimal(value);
				}
			}
			public System.String Credit
			{
				get
				{
					System.Decimal? data = entity.Credit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Credit = null;
					else entity.Credit = Convert.ToDecimal(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String DocumentNumber
			{
				get
				{
					System.String data = entity.DocumentNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentNumber = null;
					else entity.DocumentNumber = Convert.ToString(value);
				}
			}
			public System.String DocumentPageNo
			{
				get
				{
					System.Int32? data = entity.DocumentPageNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentPageNo = null;
					else entity.DocumentPageNo = Convert.ToInt32(value);
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
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
			public System.String SupplierID
			{
				get
				{
					System.String data = entity.SupplierID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupplierID = null;
					else entity.SupplierID = Convert.ToString(value);
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
			public System.String DueDate
			{
				get
				{
					System.DateTime? data = entity.DueDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DueDate = null;
					else entity.DueDate = Convert.ToDateTime(value);
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
			public System.String DocumentNumberSequenceNo
			{
				get
				{
					System.String data = entity.DocumentNumberSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentNumberSequenceNo = null;
					else entity.DocumentNumberSequenceNo = Convert.ToString(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
			public System.String IsProrataDiscToRevenue
			{
				get
				{
					System.Boolean? data = entity.IsProrataDiscToRevenue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProrataDiscToRevenue = null;
					else entity.IsProrataDiscToRevenue = Convert.ToBoolean(value);
				}
			}
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
			private esJournalTransactionDetails entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalTransactionDetailsQuery query)
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
				throw new Exception("esJournalTransactionDetails can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class JournalTransactionDetails : esJournalTransactionDetails
	{	
	}

	[Serializable]
	abstract public class esJournalTransactionDetailsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return JournalTransactionDetailsMetadata.Meta();
			}
		}	
			
		public esQueryItem DetailId
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.DetailId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
			
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DocumentNumber
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.DocumentNumber, esSystemType.String);
			}
		} 
			
		public esQueryItem DocumentPageNo
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.DocumentPageNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem SupplierID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.SupplierID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem DueDate
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.DueDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem DocumentNumberSequenceNo
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.DocumentNumberSequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsProrataDiscToRevenue
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.IsProrataDiscToRevenue, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionDetailsMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalTransactionDetailsCollection")]
	public partial class JournalTransactionDetailsCollection : esJournalTransactionDetailsCollection, IEnumerable< JournalTransactionDetails>
	{
		public JournalTransactionDetailsCollection()
		{

		}	
		
		public static implicit operator List< JournalTransactionDetails>(JournalTransactionDetailsCollection coll)
		{
			List< JournalTransactionDetails> list = new List< JournalTransactionDetails>();
			
			foreach (JournalTransactionDetails emp in coll)
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
				return  JournalTransactionDetailsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalTransactionDetailsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalTransactionDetails(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalTransactionDetails();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public JournalTransactionDetailsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalTransactionDetailsQuery();
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
		public bool Load(JournalTransactionDetailsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public JournalTransactionDetails AddNew()
		{
			JournalTransactionDetails entity = base.AddNewEntity() as JournalTransactionDetails;
			
			return entity;		
		}
		public JournalTransactionDetails FindByPrimaryKey(Int32 detailId)
		{
			return base.FindByPrimaryKey(detailId) as JournalTransactionDetails;
		}

		#region IEnumerable< JournalTransactionDetails> Members

		IEnumerator< JournalTransactionDetails> IEnumerable< JournalTransactionDetails>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalTransactionDetails;
			}
		}

		#endregion
		
		private JournalTransactionDetailsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalTransactionDetails' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("JournalTransactionDetails ({DetailId})")]
	[Serializable]
	public partial class JournalTransactionDetails : esJournalTransactionDetails
	{
		public JournalTransactionDetails()
		{
		}	
	
		public JournalTransactionDetails(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalTransactionDetailsMetadata.Meta();
			}
		}	
	
		override protected esJournalTransactionDetailsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalTransactionDetailsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public JournalTransactionDetailsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalTransactionDetailsQuery();
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
		public bool Load(JournalTransactionDetailsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private JournalTransactionDetailsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class JournalTransactionDetailsQuery : esJournalTransactionDetailsQuery
	{
		public JournalTransactionDetailsQuery()
		{

		}		
		
		public JournalTransactionDetailsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "JournalTransactionDetailsQuery";
        }
	}

	[Serializable]
	public partial class JournalTransactionDetailsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalTransactionDetailsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.DetailId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.DetailId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.JournalId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.Debit, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.Debit;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.Credit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.Credit;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.Description, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.SubLedgerId, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.DocumentNumber, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.DocumentNumber;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.DocumentPageNo, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.DocumentPageNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.ClassID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.GuarantorID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.SupplierID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.SupplierID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.ParamedicID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.DueDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.DueDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.ServiceUnitID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.ItemID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.DocumentNumberSequenceNo, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.DocumentNumberSequenceNo;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.RegistrationNo, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.DateCreated, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.DateCreated;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.CreatedBy, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.IsProrataDiscToRevenue, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.IsProrataDiscToRevenue;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionDetailsMetadata.ColumnNames.TariffComponentID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionDetailsMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public JournalTransactionDetailsMetadata Meta()
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
			public const string DetailId = "DetailId";
			public const string JournalId = "JournalId";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string Description = "Description";
			public const string SubLedgerId = "SubLedgerId";
			public const string DocumentNumber = "DocumentNumber";
			public const string DocumentPageNo = "DocumentPageNo";
			public const string ClassID = "ClassID";
			public const string GuarantorID = "GuarantorID";
			public const string SupplierID = "SupplierID";
			public const string ParamedicID = "ParamedicID";
			public const string DueDate = "DueDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string DocumentNumberSequenceNo = "DocumentNumberSequenceNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string DateCreated = "DateCreated";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsProrataDiscToRevenue = "IsProrataDiscToRevenue";
			public const string TariffComponentID = "TariffComponentID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string DetailId = "DetailId";
			public const string JournalId = "JournalId";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string Description = "Description";
			public const string SubLedgerId = "SubLedgerId";
			public const string DocumentNumber = "DocumentNumber";
			public const string DocumentPageNo = "DocumentPageNo";
			public const string ClassID = "ClassID";
			public const string GuarantorID = "GuarantorID";
			public const string SupplierID = "SupplierID";
			public const string ParamedicID = "ParamedicID";
			public const string DueDate = "DueDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string DocumentNumberSequenceNo = "DocumentNumberSequenceNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string DateCreated = "DateCreated";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsProrataDiscToRevenue = "IsProrataDiscToRevenue";
			public const string TariffComponentID = "TariffComponentID";
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
			lock (typeof(JournalTransactionDetailsMetadata))
			{
				if(JournalTransactionDetailsMetadata.mapDelegates == null)
				{
					JournalTransactionDetailsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalTransactionDetailsMetadata.meta == null)
				{
					JournalTransactionDetailsMetadata.meta = new JournalTransactionDetailsMetadata();
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
				
				meta.AddTypeMap("DetailId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Debit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DocumentNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentPageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DueDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentNumberSequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProrataDiscToRevenue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "JournalTransactionDetails";
				meta.Destination = "JournalTransactionDetails";
				meta.spInsert = "proc_JournalTransactionDetailsInsert";				
				meta.spUpdate = "proc_JournalTransactionDetailsUpdate";		
				meta.spDelete = "proc_JournalTransactionDetailsDelete";
				meta.spLoadAll = "proc_JournalTransactionDetailsLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalTransactionDetailsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalTransactionDetailsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
