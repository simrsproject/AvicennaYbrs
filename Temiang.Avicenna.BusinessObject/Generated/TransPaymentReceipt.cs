/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/19/2012 3:48:39 PM
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
	abstract public class esTransPaymentReceiptCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentReceiptCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPaymentReceiptCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentReceiptQuery query)
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
			this.InitQuery(query as esTransPaymentReceiptQuery);
		}
		#endregion
		
		virtual public TransPaymentReceipt DetachEntity(TransPaymentReceipt entity)
		{
			return base.DetachEntity(entity) as TransPaymentReceipt;
		}
		
		virtual public TransPaymentReceipt AttachEntity(TransPaymentReceipt entity)
		{
			return base.AttachEntity(entity) as TransPaymentReceipt;
		}
		
		virtual public void Combine(TransPaymentReceiptCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPaymentReceipt this[int index]
		{
			get
			{
				return base[index] as TransPaymentReceipt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentReceipt);
		}
	}



	[Serializable]
	abstract public class esTransPaymentReceipt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentReceiptQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPaymentReceipt()
		{

		}

		public esTransPaymentReceipt(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paymentReceiptNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentReceiptNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentReceiptNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentReceiptNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentReceiptNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentReceiptNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paymentReceiptNo)
		{
			esTransPaymentReceiptQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentReceiptNo == paymentReceiptNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paymentReceiptNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentReceiptNo",paymentReceiptNo);
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
						case "PaymentReceiptNo": this.str.PaymentReceiptNo = (string)value; break;							
						case "PaymentReceiptDate": this.str.PaymentReceiptDate = (string)value; break;							
						case "PaymentReceiptTime": this.str.PaymentReceiptTime = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "PrintReceiptAsName": this.str.PrintReceiptAsName = (string)value; break;							
						case "PrintNumber": this.str.PrintNumber = (string)value; break;							
						case "IsPrinted": this.str.IsPrinted = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PaymentReceiptDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentReceiptDate = (System.DateTime?)value;
							break;
						
						case "PrintNumber":
						
							if (value == null || value is System.Byte)
								this.PrintNumber = (System.Byte?)value;
							break;
						
						case "IsPrinted":
						
							if (value == null || value is System.Boolean)
								this.IsPrinted = (System.Boolean?)value;
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
		/// Maps to TransPaymentReceipt.PaymentReceiptNo
		/// </summary>
		virtual public System.String PaymentReceiptNo
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.PaymentReceiptDate
		/// </summary>
		virtual public System.DateTime? PaymentReceiptDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.PaymentReceiptTime
		/// </summary>
		virtual public System.String PaymentReceiptTime
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptTime);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.PrintReceiptAsName
		/// </summary>
		virtual public System.String PrintReceiptAsName
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.PrintReceiptAsName);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.PrintReceiptAsName, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.PrintNumber
		/// </summary>
		virtual public System.Byte? PrintNumber
		{
			get
			{
				return base.GetSystemByte(TransPaymentReceiptMetadata.ColumnNames.PrintNumber);
			}
			
			set
			{
				base.SetSystemByte(TransPaymentReceiptMetadata.ColumnNames.PrintNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.IsPrinted
		/// </summary>
		virtual public System.Boolean? IsPrinted
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentReceiptMetadata.ColumnNames.IsPrinted);
			}
			
			set
			{
				base.SetSystemBoolean(TransPaymentReceiptMetadata.ColumnNames.IsPrinted, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentReceiptMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(TransPaymentReceiptMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentReceiptMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransPaymentReceiptMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentReceiptMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentReceipt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentReceiptMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentReceiptMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTransPaymentReceipt entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PaymentReceiptNo
			{
				get
				{
					System.String data = entity.PaymentReceiptNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReceiptNo = null;
					else entity.PaymentReceiptNo = Convert.ToString(value);
				}
			}
				
			public System.String PaymentReceiptDate
			{
				get
				{
					System.DateTime? data = entity.PaymentReceiptDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReceiptDate = null;
					else entity.PaymentReceiptDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String PaymentReceiptTime
			{
				get
				{
					System.String data = entity.PaymentReceiptTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReceiptTime = null;
					else entity.PaymentReceiptTime = Convert.ToString(value);
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
				
			public System.String PrintReceiptAsName
			{
				get
				{
					System.String data = entity.PrintReceiptAsName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintReceiptAsName = null;
					else entity.PrintReceiptAsName = Convert.ToString(value);
				}
			}
				
			public System.String PrintNumber
			{
				get
				{
					System.Byte? data = entity.PrintNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintNumber = null;
					else entity.PrintNumber = Convert.ToByte(value);
				}
			}
				
			public System.String IsPrinted
			{
				get
				{
					System.Boolean? data = entity.IsPrinted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrinted = null;
					else entity.IsPrinted = Convert.ToBoolean(value);
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
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esTransPaymentReceipt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentReceiptQuery query)
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
				throw new Exception("esTransPaymentReceipt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransPaymentReceipt : esTransPaymentReceipt
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
	abstract public class esTransPaymentReceiptQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentReceiptMetadata.Meta();
			}
		}	
		

		public esQueryItem PaymentReceiptNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentReceiptDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PaymentReceiptTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptTime, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PrintReceiptAsName
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.PrintReceiptAsName, esSystemType.String);
			}
		} 
		
		public esQueryItem PrintNumber
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.PrintNumber, esSystemType.Byte);
			}
		} 
		
		public esQueryItem IsPrinted
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.IsPrinted, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentReceiptMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentReceiptCollection")]
	public partial class TransPaymentReceiptCollection : esTransPaymentReceiptCollection, IEnumerable<TransPaymentReceipt>
	{
		public TransPaymentReceiptCollection()
		{

		}
		
		public static implicit operator List<TransPaymentReceipt>(TransPaymentReceiptCollection coll)
		{
			List<TransPaymentReceipt> list = new List<TransPaymentReceipt>();
			
			foreach (TransPaymentReceipt emp in coll)
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
				return  TransPaymentReceiptMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentReceiptQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentReceipt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentReceipt();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPaymentReceiptQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentReceiptQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPaymentReceiptQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPaymentReceipt AddNew()
		{
			TransPaymentReceipt entity = base.AddNewEntity() as TransPaymentReceipt;
			
			return entity;
		}

		public TransPaymentReceipt FindByPrimaryKey(System.String paymentReceiptNo)
		{
			return base.FindByPrimaryKey(paymentReceiptNo) as TransPaymentReceipt;
		}


		#region IEnumerable<TransPaymentReceipt> Members

		IEnumerator<TransPaymentReceipt> IEnumerable<TransPaymentReceipt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentReceipt;
			}
		}

		#endregion
		
		private TransPaymentReceiptQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentReceipt' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPaymentReceipt ({PaymentReceiptNo})")]
	[Serializable]
	public partial class TransPaymentReceipt : esTransPaymentReceipt
	{
		public TransPaymentReceipt()
		{

		}
	
		public TransPaymentReceipt(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentReceiptMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPaymentReceiptQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentReceiptQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPaymentReceiptQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentReceiptQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPaymentReceiptQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPaymentReceiptQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPaymentReceiptQuery : esTransPaymentReceiptQuery
	{
		public TransPaymentReceiptQuery()
		{

		}		
		
		public TransPaymentReceiptQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPaymentReceiptQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPaymentReceiptMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentReceiptMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.PaymentReceiptNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.PaymentReceiptDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.PaymentReceiptTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.PrintReceiptAsName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.PrintReceiptAsName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.PrintNumber, 5, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.PrintNumber;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.IsPrinted, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.IsPrinted;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.ApprovedDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.ApprovedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.IsVoid, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.VoidDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.VoidByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.Notes, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentReceiptMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentReceiptMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPaymentReceiptMetadata Meta()
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
			 public const string PaymentReceiptNo = "PaymentReceiptNo";
			 public const string PaymentReceiptDate = "PaymentReceiptDate";
			 public const string PaymentReceiptTime = "PaymentReceiptTime";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string PrintReceiptAsName = "PrintReceiptAsName";
			 public const string PrintNumber = "PrintNumber";
			 public const string IsPrinted = "IsPrinted";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PaymentReceiptNo = "PaymentReceiptNo";
			 public const string PaymentReceiptDate = "PaymentReceiptDate";
			 public const string PaymentReceiptTime = "PaymentReceiptTime";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string PrintReceiptAsName = "PrintReceiptAsName";
			 public const string PrintNumber = "PrintNumber";
			 public const string IsPrinted = "IsPrinted";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string Notes = "Notes";
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
			lock (typeof(TransPaymentReceiptMetadata))
			{
				if(TransPaymentReceiptMetadata.mapDelegates == null)
				{
					TransPaymentReceiptMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPaymentReceiptMetadata.meta == null)
				{
					TransPaymentReceiptMetadata.meta = new TransPaymentReceiptMetadata();
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
				

				meta.AddTypeMap("PaymentReceiptNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentReceiptDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PaymentReceiptTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrintReceiptAsName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrintNumber", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("IsPrinted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransPaymentReceipt";
				meta.Destination = "TransPaymentReceipt";
				
				meta.spInsert = "proc_TransPaymentReceiptInsert";				
				meta.spUpdate = "proc_TransPaymentReceiptUpdate";		
				meta.spDelete = "proc_TransPaymentReceiptDelete";
				meta.spLoadAll = "proc_TransPaymentReceiptLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentReceiptLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentReceiptMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
