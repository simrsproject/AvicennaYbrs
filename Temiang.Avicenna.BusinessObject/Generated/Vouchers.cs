/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:28 PM
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
	abstract public class esVouchersCollection : esEntityCollectionWAuditLog
	{
		public esVouchersCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VouchersCollection";
		}

		#region Query Logic
		protected void InitQuery(esVouchersQuery query)
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
			this.InitQuery(query as esVouchersQuery);
		}
		#endregion
		
		virtual public Vouchers DetachEntity(Vouchers entity)
		{
			return base.DetachEntity(entity) as Vouchers;
		}
		
		virtual public Vouchers AttachEntity(Vouchers entity)
		{
			return base.AttachEntity(entity) as Vouchers;
		}
		
		virtual public void Combine(VouchersCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Vouchers this[int index]
		{
			get
			{
				return base[index] as Vouchers;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Vouchers);
		}
	}



	[Serializable]
	abstract public class esVouchers : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVouchersQuery GetDynamicQuery()
		{
			return null;
		}

		public esVouchers()
		{

		}

		public esVouchers(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String voucherID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(voucherID);
			else
				return LoadByPrimaryKeyStoredProcedure(voucherID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String voucherID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(voucherID);
			else
				return LoadByPrimaryKeyStoredProcedure(voucherID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String voucherID)
		{
			esVouchersQuery query = this.GetDynamicQuery();
			query.Where(query.VoucherID == voucherID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String voucherID)
		{
			esParameters parms = new esParameters();
			parms.Add("VoucherID",voucherID);
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
						case "VoucherID": this.str.VoucherID = (string)value; break;							
						case "VoucherDate": this.str.VoucherDate = (string)value; break;							
						case "SRVoucherType": this.str.SRVoucherType = (string)value; break;							
						case "VoucherYear": this.str.VoucherYear = (string)value; break;							
						case "VoucherMonth": this.str.VoucherMonth = (string)value; break;							
						case "VoucherCode": this.str.VoucherCode = (string)value; break;							
						case "VoucherNumber": this.str.VoucherNumber = (string)value; break;							
						case "VoucherNotes": this.str.VoucherNotes = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "TotalAmount": this.str.TotalAmount = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "EntryDateTime": this.str.EntryDateTime = (string)value; break;							
						case "EntryByUserID": this.str.EntryByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "VoucherDate":
						
							if (value == null || value is System.DateTime)
								this.VoucherDate = (System.DateTime?)value;
							break;
						
						case "TotalAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalAmount = (System.Decimal?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "ApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						
						case "EntryDateTime":
						
							if (value == null || value is System.DateTime)
								this.EntryDateTime = (System.DateTime?)value;
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
		/// Maps to Vouchers.VoucherID
		/// </summary>
		virtual public System.String VoucherID
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoucherID);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoucherID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoucherDate
		/// </summary>
		virtual public System.DateTime? VoucherDate
		{
			get
			{
				return base.GetSystemDateTime(VouchersMetadata.ColumnNames.VoucherDate);
			}
			
			set
			{
				base.SetSystemDateTime(VouchersMetadata.ColumnNames.VoucherDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.SRVoucherType
		/// </summary>
		virtual public System.String SRVoucherType
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.SRVoucherType);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.SRVoucherType, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoucherYear
		/// </summary>
		virtual public System.String VoucherYear
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoucherYear);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoucherYear, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoucherMonth
		/// </summary>
		virtual public System.String VoucherMonth
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoucherMonth);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoucherMonth, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoucherCode
		/// </summary>
		virtual public System.String VoucherCode
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoucherCode);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoucherCode, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoucherNumber
		/// </summary>
		virtual public System.String VoucherNumber
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoucherNumber);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoucherNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoucherNotes
		/// </summary>
		virtual public System.String VoucherNotes
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoucherNotes);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoucherNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.TotalAmount
		/// </summary>
		virtual public System.Decimal? TotalAmount
		{
			get
			{
				return base.GetSystemDecimal(VouchersMetadata.ColumnNames.TotalAmount);
			}
			
			set
			{
				base.SetSystemDecimal(VouchersMetadata.ColumnNames.TotalAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(VouchersMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(VouchersMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(VouchersMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(VouchersMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(VouchersMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(VouchersMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(VouchersMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(VouchersMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.EntryDateTime
		/// </summary>
		virtual public System.DateTime? EntryDateTime
		{
			get
			{
				return base.GetSystemDateTime(VouchersMetadata.ColumnNames.EntryDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VouchersMetadata.ColumnNames.EntryDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.EntryByUserID
		/// </summary>
		virtual public System.String EntryByUserID
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.EntryByUserID);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.EntryByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VouchersMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VouchersMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Vouchers.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VouchersMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VouchersMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esVouchers entity)
			{
				this.entity = entity;
			}
			
	
			public System.String VoucherID
			{
				get
				{
					System.String data = entity.VoucherID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherID = null;
					else entity.VoucherID = Convert.ToString(value);
				}
			}
				
			public System.String VoucherDate
			{
				get
				{
					System.DateTime? data = entity.VoucherDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherDate = null;
					else entity.VoucherDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SRVoucherType
			{
				get
				{
					System.String data = entity.SRVoucherType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVoucherType = null;
					else entity.SRVoucherType = Convert.ToString(value);
				}
			}
				
			public System.String VoucherYear
			{
				get
				{
					System.String data = entity.VoucherYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherYear = null;
					else entity.VoucherYear = Convert.ToString(value);
				}
			}
				
			public System.String VoucherMonth
			{
				get
				{
					System.String data = entity.VoucherMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherMonth = null;
					else entity.VoucherMonth = Convert.ToString(value);
				}
			}
				
			public System.String VoucherCode
			{
				get
				{
					System.String data = entity.VoucherCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherCode = null;
					else entity.VoucherCode = Convert.ToString(value);
				}
			}
				
			public System.String VoucherNumber
			{
				get
				{
					System.String data = entity.VoucherNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherNumber = null;
					else entity.VoucherNumber = Convert.ToString(value);
				}
			}
				
			public System.String VoucherNotes
			{
				get
				{
					System.String data = entity.VoucherNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherNotes = null;
					else entity.VoucherNotes = Convert.ToString(value);
				}
			}
				
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
				
			public System.String TotalAmount
			{
				get
				{
					System.Decimal? data = entity.TotalAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalAmount = null;
					else entity.TotalAmount = Convert.ToDecimal(value);
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
				
			public System.String ApprovedDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDate = null;
					else entity.ApprovedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
				
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
				
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EntryDateTime
			{
				get
				{
					System.DateTime? data = entity.EntryDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EntryDateTime = null;
					else entity.EntryDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String EntryByUserID
			{
				get
				{
					System.String data = entity.EntryByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EntryByUserID = null;
					else entity.EntryByUserID = Convert.ToString(value);
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
			

			private esVouchers entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVouchersQuery query)
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
				throw new Exception("esVouchers can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Vouchers : esVouchers
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
	abstract public class esVouchersQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VouchersMetadata.Meta();
			}
		}	
		

		public esQueryItem VoucherID
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherID, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherDate
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRVoucherType
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.SRVoucherType, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherYear
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherYear, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherMonth
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherMonth, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherCode
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherCode, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherNumber
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherNotes
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoucherNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem TotalAmount
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.TotalAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EntryDateTime
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.EntryDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EntryByUserID
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.EntryByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VouchersMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VouchersCollection")]
	public partial class VouchersCollection : esVouchersCollection, IEnumerable<Vouchers>
	{
		public VouchersCollection()
		{

		}
		
		public static implicit operator List<Vouchers>(VouchersCollection coll)
		{
			List<Vouchers> list = new List<Vouchers>();
			
			foreach (Vouchers emp in coll)
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
				return  VouchersMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VouchersQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Vouchers(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Vouchers();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public VouchersQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VouchersQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VouchersQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Vouchers AddNew()
		{
			Vouchers entity = base.AddNewEntity() as Vouchers;
			
			return entity;
		}

		public Vouchers FindByPrimaryKey(System.String voucherID)
		{
			return base.FindByPrimaryKey(voucherID) as Vouchers;
		}


		#region IEnumerable<Vouchers> Members

		IEnumerator<Vouchers> IEnumerable<Vouchers>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Vouchers;
			}
		}

		#endregion
		
		private VouchersQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Vouchers' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Vouchers ({VoucherID})")]
	[Serializable]
	public partial class Vouchers : esVouchers
	{
		public Vouchers()
		{

		}
	
		public Vouchers(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VouchersMetadata.Meta();
			}
		}
		
		
		
		override protected esVouchersQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VouchersQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VouchersQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VouchersQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VouchersQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VouchersQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VouchersQuery : esVouchersQuery
	{
		public VouchersQuery()
		{

		}		
		
		public VouchersQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VouchersQuery";
        }
		
			
	}


	[Serializable]
	public partial class VouchersMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VouchersMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.SRVoucherType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.SRVoucherType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherYear, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherMonth, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherMonth;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherNumber, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherNumber;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoucherNotes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoucherNotes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.PatientID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.TotalAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VouchersMetadata.PropertyNames.TotalAmount;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VouchersMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.ApprovedDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VouchersMetadata.PropertyNames.ApprovedDate;
			c.HasDefault = true;
			c.Default = @"(NULL)";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.ApprovedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VouchersMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.VoidDate, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VouchersMetadata.PropertyNames.VoidDate;
			c.HasDefault = true;
			c.Default = @"(NULL)";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.EntryDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VouchersMetadata.PropertyNames.EntryDateTime;
			c.HasDefault = true;
			c.Default = @"(NULL)";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.EntryByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.EntryByUserID;
			c.CharacterMaxLength = 40;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VouchersMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VouchersMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = VouchersMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VouchersMetadata Meta()
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
			 public const string VoucherID = "VoucherID";
			 public const string VoucherDate = "VoucherDate";
			 public const string SRVoucherType = "SRVoucherType";
			 public const string VoucherYear = "VoucherYear";
			 public const string VoucherMonth = "VoucherMonth";
			 public const string VoucherCode = "VoucherCode";
			 public const string VoucherNumber = "VoucherNumber";
			 public const string VoucherNotes = "VoucherNotes";
			 public const string PatientID = "PatientID";
			 public const string TotalAmount = "TotalAmount";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string VoidDate = "VoidDate";
			 public const string EntryDateTime = "EntryDateTime";
			 public const string EntryByUserID = "EntryByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string VoucherID = "VoucherID";
			 public const string VoucherDate = "VoucherDate";
			 public const string SRVoucherType = "SRVoucherType";
			 public const string VoucherYear = "VoucherYear";
			 public const string VoucherMonth = "VoucherMonth";
			 public const string VoucherCode = "VoucherCode";
			 public const string VoucherNumber = "VoucherNumber";
			 public const string VoucherNotes = "VoucherNotes";
			 public const string PatientID = "PatientID";
			 public const string TotalAmount = "TotalAmount";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string VoidDate = "VoidDate";
			 public const string EntryDateTime = "EntryDateTime";
			 public const string EntryByUserID = "EntryByUserID";
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
			lock (typeof(VouchersMetadata))
			{
				if(VouchersMetadata.mapDelegates == null)
				{
					VouchersMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VouchersMetadata.meta == null)
				{
					VouchersMetadata.meta = new VouchersMetadata();
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
				

				meta.AddTypeMap("VoucherID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRVoucherType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherMonth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TotalAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EntryDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EntryByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Vouchers";
				meta.Destination = "Vouchers";
				
				meta.spInsert = "proc_VouchersInsert";				
				meta.spUpdate = "proc_VouchersUpdate";		
				meta.spDelete = "proc_VouchersDelete";
				meta.spLoadAll = "proc_VouchersLoadAll";
				meta.spLoadByPrimaryKey = "proc_VouchersLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VouchersMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
