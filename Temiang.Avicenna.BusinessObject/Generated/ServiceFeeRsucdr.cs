/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/13/2022 5:18:32 PM
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
	abstract public class esServiceFeeRsucdrCollection : esEntityCollectionWAuditLog
	{
		public esServiceFeeRsucdrCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceFeeRsucdrCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceFeeRsucdrQuery query)
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
			this.InitQuery(query as esServiceFeeRsucdrQuery);
		}
		#endregion
			
		virtual public ServiceFeeRsucdr DetachEntity(ServiceFeeRsucdr entity)
		{
			return base.DetachEntity(entity) as ServiceFeeRsucdr;
		}
		
		virtual public ServiceFeeRsucdr AttachEntity(ServiceFeeRsucdr entity)
		{
			return base.AttachEntity(entity) as ServiceFeeRsucdr;
		}
		
		virtual public void Combine(ServiceFeeRsucdrCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceFeeRsucdr this[int index]
		{
			get
			{
				return base[index] as ServiceFeeRsucdr;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceFeeRsucdr);
		}
	}

	[Serializable]
	abstract public class esServiceFeeRsucdr : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceFeeRsucdrQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceFeeRsucdr()
		{
		}
	
		public esServiceFeeRsucdr(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			esServiceFeeRsucdrQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo, query.TariffComponentID==tariffComponentID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("TariffComponentID",tariffComponentID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "FeeDirektur": this.str.FeeDirektur = (string)value; break;
						case "FeeStruktural": this.str.FeeStruktural = (string)value; break;
						case "FeeMedis": this.str.FeeMedis = (string)value; break;
						case "FeeUnit": this.str.FeeUnit = (string)value; break;
						case "FeePemerataan": this.str.FeePemerataan = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsParamedicFeeRemun": this.str.IsParamedicFeeRemun = (string)value; break;
						case "IsParamedicEmergency": this.str.IsParamedicEmergency = (string)value; break;
						case "RemunID": this.str.RemunID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "FeeDirektur":
						
							if (value == null || value is System.Decimal)
								this.FeeDirektur = (System.Decimal?)value;
							break;
						case "FeeStruktural":
						
							if (value == null || value is System.Decimal)
								this.FeeStruktural = (System.Decimal?)value;
							break;
						case "FeeMedis":
						
							if (value == null || value is System.Decimal)
								this.FeeMedis = (System.Decimal?)value;
							break;
						case "FeeUnit":
						
							if (value == null || value is System.Decimal)
								this.FeeUnit = (System.Decimal?)value;
							break;
						case "FeePemerataan":
						
							if (value == null || value is System.Decimal)
								this.FeePemerataan = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsParamedicFeeRemun":
						
							if (value == null || value is System.Boolean)
								this.IsParamedicFeeRemun = (System.Boolean?)value;
							break;
						case "IsParamedicEmergency":
						
							if (value == null || value is System.Boolean)
								this.IsParamedicEmergency = (System.Boolean?)value;
							break;
						case "RemunID":
						
							if (value == null || value is System.Int32)
								this.RemunID = (System.Int32?)value;
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
		/// Maps to ServiceFeeRsucdr.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.FeeDirektur
		/// </summary>
		virtual public System.Decimal? FeeDirektur
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeDirektur);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeDirektur, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.FeeStruktural
		/// </summary>
		virtual public System.Decimal? FeeStruktural
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeStruktural);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeStruktural, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.FeeMedis
		/// </summary>
		virtual public System.Decimal? FeeMedis
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeMedis);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeMedis, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.FeeUnit
		/// </summary>
		virtual public System.Decimal? FeeUnit
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeUnit);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeeUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.FeePemerataan
		/// </summary>
		virtual public System.Decimal? FeePemerataan
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeePemerataan);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRsucdrMetadata.ColumnNames.FeePemerataan, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRsucdrMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRsucdrMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.IsParamedicFeeRemun
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeRemun
		{
			get
			{
				return base.GetSystemBoolean(ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicFeeRemun);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicFeeRemun, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.IsParamedicEmergency
		/// </summary>
		virtual public System.Boolean? IsParamedicEmergency
		{
			get
			{
				return base.GetSystemBoolean(ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicEmergency);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicEmergency, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRsucdr.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeRsucdrMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeRsucdrMetadata.ColumnNames.RemunID, value);
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
			public esStrings(esServiceFeeRsucdr entity)
			{
				this.entity = entity;
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
			public System.String FeeDirektur
			{
				get
				{
					System.Decimal? data = entity.FeeDirektur;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeDirektur = null;
					else entity.FeeDirektur = Convert.ToDecimal(value);
				}
			}
			public System.String FeeStruktural
			{
				get
				{
					System.Decimal? data = entity.FeeStruktural;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeStruktural = null;
					else entity.FeeStruktural = Convert.ToDecimal(value);
				}
			}
			public System.String FeeMedis
			{
				get
				{
					System.Decimal? data = entity.FeeMedis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeMedis = null;
					else entity.FeeMedis = Convert.ToDecimal(value);
				}
			}
			public System.String FeeUnit
			{
				get
				{
					System.Decimal? data = entity.FeeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeUnit = null;
					else entity.FeeUnit = Convert.ToDecimal(value);
				}
			}
			public System.String FeePemerataan
			{
				get
				{
					System.Decimal? data = entity.FeePemerataan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeePemerataan = null;
					else entity.FeePemerataan = Convert.ToDecimal(value);
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
			public System.String IsParamedicFeeRemun
			{
				get
				{
					System.Boolean? data = entity.IsParamedicFeeRemun;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParamedicFeeRemun = null;
					else entity.IsParamedicFeeRemun = Convert.ToBoolean(value);
				}
			}
			public System.String IsParamedicEmergency
			{
				get
				{
					System.Boolean? data = entity.IsParamedicEmergency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParamedicEmergency = null;
					else entity.IsParamedicEmergency = Convert.ToBoolean(value);
				}
			}
			public System.String RemunID
			{
				get
				{
					System.Int32? data = entity.RemunID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunID = null;
					else entity.RemunID = Convert.ToInt32(value);
				}
			}
			private esServiceFeeRsucdr entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceFeeRsucdrQuery query)
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
				throw new Exception("esServiceFeeRsucdr can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceFeeRsucdr : esServiceFeeRsucdr
	{	
	}

	[Serializable]
	abstract public class esServiceFeeRsucdrQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRsucdrMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem FeeDirektur
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.FeeDirektur, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeStruktural
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.FeeStruktural, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeMedis
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.FeeMedis, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeUnit
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.FeeUnit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeePemerataan
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.FeePemerataan, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsParamedicFeeRemun
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicFeeRemun, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsParamedicEmergency
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicEmergency, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRsucdrMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceFeeRsucdrCollection")]
	public partial class ServiceFeeRsucdrCollection : esServiceFeeRsucdrCollection, IEnumerable< ServiceFeeRsucdr>
	{
		public ServiceFeeRsucdrCollection()
		{

		}	
		
		public static implicit operator List< ServiceFeeRsucdr>(ServiceFeeRsucdrCollection coll)
		{
			List< ServiceFeeRsucdr> list = new List< ServiceFeeRsucdr>();
			
			foreach (ServiceFeeRsucdr emp in coll)
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
				return  ServiceFeeRsucdrMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRsucdrQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceFeeRsucdr(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceFeeRsucdr();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceFeeRsucdrQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRsucdrQuery();
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
		public bool Load(ServiceFeeRsucdrQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceFeeRsucdr AddNew()
		{
			ServiceFeeRsucdr entity = base.AddNewEntity() as ServiceFeeRsucdr;
			
			return entity;		
		}
		public ServiceFeeRsucdr FindByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID) as ServiceFeeRsucdr;
		}

		#region IEnumerable< ServiceFeeRsucdr> Members

		IEnumerator< ServiceFeeRsucdr> IEnumerable< ServiceFeeRsucdr>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceFeeRsucdr;
			}
		}

		#endregion
		
		private ServiceFeeRsucdrQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceFeeRsucdr' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceFeeRsucdr ({TransactionNo, SequenceNo, TariffComponentID})")]
	[Serializable]
	public partial class ServiceFeeRsucdr : esServiceFeeRsucdr
	{
		public ServiceFeeRsucdr()
		{
		}	
	
		public ServiceFeeRsucdr(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRsucdrMetadata.Meta();
			}
		}	
	
		override protected esServiceFeeRsucdrQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRsucdrQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceFeeRsucdrQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRsucdrQuery();
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
		public bool Load(ServiceFeeRsucdrQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceFeeRsucdrQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceFeeRsucdrQuery : esServiceFeeRsucdrQuery
	{
		public ServiceFeeRsucdrQuery()
		{

		}		
		
		public ServiceFeeRsucdrQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceFeeRsucdrQuery";
        }
	}

	[Serializable]
	public partial class ServiceFeeRsucdrMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceFeeRsucdrMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.FeeDirektur, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.FeeDirektur;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.FeeStruktural, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.FeeStruktural;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.FeeMedis, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.FeeMedis;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.FeeUnit, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.FeeUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.FeePemerataan, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.FeePemerataan;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.CreateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.CreateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicFeeRemun, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.IsParamedicFeeRemun;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.IsParamedicEmergency, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.IsParamedicEmergency;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRsucdrMetadata.ColumnNames.RemunID, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeRsucdrMetadata.PropertyNames.RemunID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceFeeRsucdrMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string RegistrationNo = "RegistrationNo";
			public const string FeeDirektur = "FeeDirektur";
			public const string FeeStruktural = "FeeStruktural";
			public const string FeeMedis = "FeeMedis";
			public const string FeeUnit = "FeeUnit";
			public const string FeePemerataan = "FeePemerataan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsParamedicFeeRemun = "IsParamedicFeeRemun";
			public const string IsParamedicEmergency = "IsParamedicEmergency";
			public const string RemunID = "RemunID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string RegistrationNo = "RegistrationNo";
			public const string FeeDirektur = "FeeDirektur";
			public const string FeeStruktural = "FeeStruktural";
			public const string FeeMedis = "FeeMedis";
			public const string FeeUnit = "FeeUnit";
			public const string FeePemerataan = "FeePemerataan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsParamedicFeeRemun = "IsParamedicFeeRemun";
			public const string IsParamedicEmergency = "IsParamedicEmergency";
			public const string RemunID = "RemunID";
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
			lock (typeof(ServiceFeeRsucdrMetadata))
			{
				if(ServiceFeeRsucdrMetadata.mapDelegates == null)
				{
					ServiceFeeRsucdrMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceFeeRsucdrMetadata.meta == null)
				{
					ServiceFeeRsucdrMetadata.meta = new ServiceFeeRsucdrMetadata();
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
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FeeDirektur", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FeeStruktural", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FeeMedis", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FeeUnit", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FeePemerataan", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsParamedicFeeRemun", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsParamedicEmergency", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RemunID", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "ServiceFeeRsucdr";
				meta.Destination = "ServiceFeeRsucdr";
				meta.spInsert = "proc_ServiceFeeRsucdrInsert";				
				meta.spUpdate = "proc_ServiceFeeRsucdrUpdate";		
				meta.spDelete = "proc_ServiceFeeRsucdrDelete";
				meta.spLoadAll = "proc_ServiceFeeRsucdrLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceFeeRsucdrLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceFeeRsucdrMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
