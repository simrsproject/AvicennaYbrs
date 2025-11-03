/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/4/2022 11:44:08 AM
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
	abstract public class esRenkinTransactionCollection : esEntityCollectionWAuditLog
	{
		public esRenkinTransactionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RenkinTransactionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRenkinTransactionQuery query)
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
			this.InitQuery(query as esRenkinTransactionQuery);
		}
		#endregion
			
		virtual public RenkinTransaction DetachEntity(RenkinTransaction entity)
		{
			return base.DetachEntity(entity) as RenkinTransaction;
		}
		
		virtual public RenkinTransaction AttachEntity(RenkinTransaction entity)
		{
			return base.AttachEntity(entity) as RenkinTransaction;
		}
		
		virtual public void Combine(RenkinTransactionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RenkinTransaction this[int index]
		{
			get
			{
				return base[index] as RenkinTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RenkinTransaction);
		}
	}

	[Serializable]
	abstract public class esRenkinTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRenkinTransactionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRenkinTransaction()
		{
		}
	
		public esRenkinTransaction(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 transactionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 transactionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 transactionID)
		{
			esRenkinTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionID==transactionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 transactionID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionID",transactionID);
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
						case "TransactionID": this.str.TransactionID = (string)value; break;
						case "PeriodeID": this.str.PeriodeID = (string)value; break;
						case "EmployeePositionID": this.str.EmployeePositionID = (string)value; break;
						case "SRRenkinTransactionStatus": this.str.SRRenkinTransactionStatus = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsApprove": this.str.IsApprove = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "VoidNotes": this.str.VoidNotes = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionID":
						
							if (value == null || value is System.Int32)
								this.TransactionID = (System.Int32?)value;
							break;
						case "PeriodeID":
						
							if (value == null || value is System.Int32)
								this.PeriodeID = (System.Int32?)value;
							break;
						case "EmployeePositionID":
						
							if (value == null || value is System.Int32)
								this.EmployeePositionID = (System.Int32?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsApprove":
						
							if (value == null || value is System.Boolean)
								this.IsApprove = (System.Boolean?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "VerifiedDateTime":
						
							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "VoidDateTime":
						
							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
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
		/// Maps to RenkinTransaction.TransactionID
		/// </summary>
		virtual public System.Int32? TransactionID
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionMetadata.ColumnNames.TransactionID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionMetadata.ColumnNames.TransactionID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.PeriodeID
		/// </summary>
		virtual public System.Int32? PeriodeID
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionMetadata.ColumnNames.PeriodeID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionMetadata.ColumnNames.PeriodeID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.EmployeePositionID
		/// </summary>
		virtual public System.Int32? EmployeePositionID
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionMetadata.ColumnNames.EmployeePositionID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionMetadata.ColumnNames.EmployeePositionID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.SRRenkinTransactionStatus
		/// </summary>
		virtual public System.String SRRenkinTransactionStatus
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.SRRenkinTransactionStatus);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.SRRenkinTransactionStatus, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(RenkinTransactionMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(RenkinTransactionMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(RenkinTransactionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(RenkinTransactionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.VerifiedByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.VoidNotes
		/// </summary>
		virtual public System.String VoidNotes
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.VoidNotes);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.VoidNotes, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RenkinTransactionMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionMetadata.ColumnNames.VerifiedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransaction.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionMetadata.ColumnNames.VoidDateTime, value);
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
			public esStrings(esRenkinTransaction entity)
			{
				this.entity = entity;
			}
			public System.String TransactionID
			{
				get
				{
					System.Int32? data = entity.TransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionID = null;
					else entity.TransactionID = Convert.ToInt32(value);
				}
			}
			public System.String PeriodeID
			{
				get
				{
					System.Int32? data = entity.PeriodeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodeID = null;
					else entity.PeriodeID = Convert.ToInt32(value);
				}
			}
			public System.String EmployeePositionID
			{
				get
				{
					System.Int32? data = entity.EmployeePositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeePositionID = null;
					else entity.EmployeePositionID = Convert.ToInt32(value);
				}
			}
			public System.String SRRenkinTransactionStatus
			{
				get
				{
					System.String data = entity.SRRenkinTransactionStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRenkinTransactionStatus = null;
					else entity.SRRenkinTransactionStatus = Convert.ToString(value);
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
			public System.String IsApprove
			{
				get
				{
					System.Boolean? data = entity.IsApprove;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApprove = null;
					else entity.IsApprove = Convert.ToBoolean(value);
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
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
				}
			}
			public System.String VoidNotes
			{
				get
				{
					System.String data = entity.VoidNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidNotes = null;
					else entity.VoidNotes = Convert.ToString(value);
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
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			private esRenkinTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRenkinTransactionQuery query)
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
				throw new Exception("esRenkinTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


    public partial class RenkinTransaction : esRenkinTransaction
    {
        public void Void(string text, string userID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
	abstract public class esRenkinTransactionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RenkinTransactionMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.TransactionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PeriodeID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.PeriodeID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem EmployeePositionID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.EmployeePositionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRRenkinTransactionStatus
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.SRRenkinTransactionStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidNotes
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.VoidNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RenkinTransactionCollection")]
	public partial class RenkinTransactionCollection : esRenkinTransactionCollection, IEnumerable< RenkinTransaction>
	{
		public RenkinTransactionCollection()
		{

		}	
		
		public static implicit operator List< RenkinTransaction>(RenkinTransactionCollection coll)
		{
			List< RenkinTransaction> list = new List< RenkinTransaction>();
			
			foreach (RenkinTransaction emp in coll)
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
				return  RenkinTransactionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RenkinTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RenkinTransaction();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RenkinTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinTransactionQuery();
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
		public bool Load(RenkinTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RenkinTransaction AddNew()
		{
			RenkinTransaction entity = base.AddNewEntity() as RenkinTransaction;
			
			return entity;		
		}
		public RenkinTransaction FindByPrimaryKey(Int32 transactionID)
		{
			return base.FindByPrimaryKey(transactionID) as RenkinTransaction;
		}

		#region IEnumerable< RenkinTransaction> Members

		IEnumerator< RenkinTransaction> IEnumerable< RenkinTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RenkinTransaction;
			}
		}

		#endregion
		
		private RenkinTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RenkinTransaction' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RenkinTransaction ({TransactionID})")]
	[Serializable]
	public partial class RenkinTransaction : esRenkinTransaction
	{
		public RenkinTransaction()
		{
		}	
	
		public RenkinTransaction(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RenkinTransactionMetadata.Meta();
			}
		}	
	
		override protected esRenkinTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RenkinTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinTransactionQuery();
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
		public bool Load(RenkinTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RenkinTransactionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RenkinTransactionQuery : esRenkinTransactionQuery
	{
		public RenkinTransactionQuery()
		{

		}		
		
		public RenkinTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RenkinTransactionQuery";
        }
	}

	[Serializable]
	public partial class RenkinTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RenkinTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.TransactionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.TransactionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.PeriodeID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.PeriodeID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.EmployeePositionID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.EmployeePositionID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.SRRenkinTransactionStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.SRRenkinTransactionStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.IsApprove, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.IsApprove;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.VerifiedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.VoidNotes, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.VoidNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.Notes, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.ApprovedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.VerifiedDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionMetadata.ColumnNames.VoidDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RenkinTransactionMetadata Meta()
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
			public const string TransactionID = "TransactionID";
			public const string PeriodeID = "PeriodeID";
			public const string EmployeePositionID = "EmployeePositionID";
			public const string SRRenkinTransactionStatus = "SRRenkinTransactionStatus";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApprove = "IsApprove";
			public const string IsVoid = "IsVoid";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string VoidByUserID = "VoidByUserID";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VoidNotes = "VoidNotes";
			public const string Notes = "Notes";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VoidDateTime = "VoidDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionID = "TransactionID";
			public const string PeriodeID = "PeriodeID";
			public const string EmployeePositionID = "EmployeePositionID";
			public const string SRRenkinTransactionStatus = "SRRenkinTransactionStatus";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApprove = "IsApprove";
			public const string IsVoid = "IsVoid";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string VoidByUserID = "VoidByUserID";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VoidNotes = "VoidNotes";
			public const string Notes = "Notes";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VoidDateTime = "VoidDateTime";
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
			lock (typeof(RenkinTransactionMetadata))
			{
				if(RenkinTransactionMetadata.mapDelegates == null)
				{
					RenkinTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RenkinTransactionMetadata.meta == null)
				{
					RenkinTransactionMetadata.meta = new RenkinTransactionMetadata();
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
				
				meta.AddTypeMap("TransactionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PeriodeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeePositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRenkinTransactionStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "RenkinTransaction";
				meta.Destination = "RenkinTransaction";
				meta.spInsert = "proc_RenkinTransactionInsert";				
				meta.spUpdate = "proc_RenkinTransactionUpdate";		
				meta.spDelete = "proc_RenkinTransactionDelete";
				meta.spLoadAll = "proc_RenkinTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_RenkinTransactionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RenkinTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
