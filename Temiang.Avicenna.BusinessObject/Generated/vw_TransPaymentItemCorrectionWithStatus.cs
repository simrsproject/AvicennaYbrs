/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/12/2018 1:21:44 PM
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
	abstract public class esvwTransPaymentItemCorrectionWithStatusCollection : esEntityCollection
	{
		public esvwTransPaymentItemCorrectionWithStatusCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "vwTransPaymentItemCorrectionWithStatusCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esvwTransPaymentItemCorrectionWithStatusQuery query)
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
			this.InitQuery(query as esvwTransPaymentItemCorrectionWithStatusQuery);
		}
		#endregion
			
		virtual public vwTransPaymentItemCorrectionWithStatus DetachEntity(vwTransPaymentItemCorrectionWithStatus entity)
		{
			return base.DetachEntity(entity) as vwTransPaymentItemCorrectionWithStatus;
		}
		
		virtual public vwTransPaymentItemCorrectionWithStatus AttachEntity(vwTransPaymentItemCorrectionWithStatus entity)
		{
			return base.AttachEntity(entity) as vwTransPaymentItemCorrectionWithStatus;
		}
		
		virtual public void Combine(vwTransPaymentItemCorrectionWithStatusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public vwTransPaymentItemCorrectionWithStatus this[int index]
		{
			get
			{
				return base[index] as vwTransPaymentItemCorrectionWithStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(vwTransPaymentItemCorrectionWithStatus);
		}
	}

	[Serializable]
	abstract public class esvwTransPaymentItemCorrectionWithStatus : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esvwTransPaymentItemCorrectionWithStatusQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esvwTransPaymentItemCorrectionWithStatus()
		{
		}
	
		public esvwTransPaymentItemCorrectionWithStatus(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey

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
						case "PaymentCorrectionNo": this.str.PaymentCorrectionNo = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;
						case "SRCardType": this.str.SRCardType = (string)value; break;
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;
						case "CardFeeAmount": this.str.CardFeeAmount = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CardFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.CardFeeAmount = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to vwTransPaymentItemCorrectionWithStatus.PaymentCorrectionNo
		/// </summary>
		virtual public System.String PaymentCorrectionNo
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentCorrectionNo);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentCorrectionNo, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentType);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentMethod);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.SRCardProvider
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardProvider);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardType);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardType, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.EDCMachineID);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.CardFeeAmount
		/// </summary>
		virtual public System.Decimal? CardFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CardFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CardFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to vwTransPaymentItemCorrectionWithStatus.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsVoid, value);
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
			public esStrings(esvwTransPaymentItemCorrectionWithStatus entity)
			{
				this.entity = entity;
			}
			public System.String PaymentCorrectionNo
			{
				get
				{
					System.String data = entity.PaymentCorrectionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentCorrectionNo = null;
					else entity.PaymentCorrectionNo = Convert.ToString(value);
				}
			}
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
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
			public System.String SRPaymentType
			{
				get
				{
					System.String data = entity.SRPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentType = null;
					else entity.SRPaymentType = Convert.ToString(value);
				}
			}
			public System.String SRPaymentMethod
			{
				get
				{
					System.String data = entity.SRPaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentMethod = null;
					else entity.SRPaymentMethod = Convert.ToString(value);
				}
			}
			public System.String SRCardProvider
			{
				get
				{
					System.String data = entity.SRCardProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardProvider = null;
					else entity.SRCardProvider = Convert.ToString(value);
				}
			}
			public System.String SRCardType
			{
				get
				{
					System.String data = entity.SRCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardType = null;
					else entity.SRCardType = Convert.ToString(value);
				}
			}
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
			public System.String CardFeeAmount
			{
				get
				{
					System.Decimal? data = entity.CardFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardFeeAmount = null;
					else entity.CardFeeAmount = Convert.ToDecimal(value);
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
			private esvwTransPaymentItemCorrectionWithStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esvwTransPaymentItemCorrectionWithStatusQuery query)
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
				throw new Exception("esvwTransPaymentItemCorrectionWithStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class vwTransPaymentItemCorrectionWithStatus : esvwTransPaymentItemCorrectionWithStatus
	{	
	}

	[Serializable]
	abstract public class esvwTransPaymentItemCorrectionWithStatusQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return vwTransPaymentItemCorrectionWithStatusMetadata.Meta();
			}
		}	
			
		public esQueryItem PaymentCorrectionNo
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentCorrectionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		} 
			
		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		} 
			
		public esQueryItem CardFeeAmount
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CardFeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("vwTransPaymentItemCorrectionWithStatusCollection")]
	public partial class vwTransPaymentItemCorrectionWithStatusCollection : esvwTransPaymentItemCorrectionWithStatusCollection, IEnumerable< vwTransPaymentItemCorrectionWithStatus>
	{
		public vwTransPaymentItemCorrectionWithStatusCollection()
		{

		}	
		
		public static implicit operator List< vwTransPaymentItemCorrectionWithStatus>(vwTransPaymentItemCorrectionWithStatusCollection coll)
		{
			List< vwTransPaymentItemCorrectionWithStatus> list = new List< vwTransPaymentItemCorrectionWithStatus>();
			
			foreach (vwTransPaymentItemCorrectionWithStatus emp in coll)
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
				return  vwTransPaymentItemCorrectionWithStatusMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vwTransPaymentItemCorrectionWithStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new vwTransPaymentItemCorrectionWithStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new vwTransPaymentItemCorrectionWithStatus();
		}
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public vwTransPaymentItemCorrectionWithStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vwTransPaymentItemCorrectionWithStatusQuery();
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
		public bool Load(vwTransPaymentItemCorrectionWithStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public vwTransPaymentItemCorrectionWithStatus AddNew()
		{
			vwTransPaymentItemCorrectionWithStatus entity = base.AddNewEntity() as vwTransPaymentItemCorrectionWithStatus;
			
			return entity;		
		}

		#region IEnumerable< vwTransPaymentItemCorrectionWithStatus> Members

		IEnumerator< vwTransPaymentItemCorrectionWithStatus> IEnumerable< vwTransPaymentItemCorrectionWithStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as vwTransPaymentItemCorrectionWithStatus;
			}
		}

		#endregion
		
		private vwTransPaymentItemCorrectionWithStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vwTransPaymentItemCorrectionWithStatus' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("vwTransPaymentItemCorrectionWithStatus ()")]
	[Serializable]
	public partial class vwTransPaymentItemCorrectionWithStatus : esvwTransPaymentItemCorrectionWithStatus
	{
		public vwTransPaymentItemCorrectionWithStatus()
		{
		}	
	
		public vwTransPaymentItemCorrectionWithStatus(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vwTransPaymentItemCorrectionWithStatusMetadata.Meta();
			}
		}	
	
		override protected esvwTransPaymentItemCorrectionWithStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vwTransPaymentItemCorrectionWithStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public vwTransPaymentItemCorrectionWithStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vwTransPaymentItemCorrectionWithStatusQuery();
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
		public bool Load(vwTransPaymentItemCorrectionWithStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private vwTransPaymentItemCorrectionWithStatusQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class vwTransPaymentItemCorrectionWithStatusQuery : esvwTransPaymentItemCorrectionWithStatusQuery
	{
		public vwTransPaymentItemCorrectionWithStatusQuery()
		{

		}		
		
		public vwTransPaymentItemCorrectionWithStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "vwTransPaymentItemCorrectionWithStatusQuery";
        }
	}

	[Serializable]
	public partial class vwTransPaymentItemCorrectionWithStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vwTransPaymentItemCorrectionWithStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentCorrectionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.PaymentCorrectionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.PaymentNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRPaymentMethod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardProvider, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.SRCardType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.SRCardType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.EDCMachineID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.EDCMachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CardFeeAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.CardFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsApproved, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.IsApproved;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransPaymentItemCorrectionWithStatusMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = vwTransPaymentItemCorrectionWithStatusMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public vwTransPaymentItemCorrectionWithStatusMetadata Meta()
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
			public const string PaymentCorrectionNo = "PaymentCorrectionNo";
			public const string PaymentNo = "PaymentNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardFeeAmount = "CardFeeAmount";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PaymentCorrectionNo = "PaymentCorrectionNo";
			public const string PaymentNo = "PaymentNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardFeeAmount = "CardFeeAmount";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
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
			lock (typeof(vwTransPaymentItemCorrectionWithStatusMetadata))
			{
				if(vwTransPaymentItemCorrectionWithStatusMetadata.mapDelegates == null)
				{
					vwTransPaymentItemCorrectionWithStatusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vwTransPaymentItemCorrectionWithStatusMetadata.meta == null)
				{
					vwTransPaymentItemCorrectionWithStatusMetadata.meta = new vwTransPaymentItemCorrectionWithStatusMetadata();
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
				
				meta.AddTypeMap("PaymentCorrectionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "vw_TransPaymentItemCorrectionWithStatus";
				meta.Destination = "vw_TransPaymentItemCorrectionWithStatus";
				meta.spInsert = "proc_vw_TransPaymentItemCorrectionWithStatusInsert";				
				meta.spUpdate = "proc_vw_TransPaymentItemCorrectionWithStatusUpdate";		
				meta.spDelete = "proc_vw_TransPaymentItemCorrectionWithStatusDelete";
				meta.spLoadAll = "proc_vw_TransPaymentItemCorrectionWithStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_TransPaymentItemCorrectionWithStatusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vwTransPaymentItemCorrectionWithStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
