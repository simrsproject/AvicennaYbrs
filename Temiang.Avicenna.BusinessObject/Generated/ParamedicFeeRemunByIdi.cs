/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/7/2021 11:05:52 AM
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
	abstract public class esParamedicFeeRemunByIdiCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeRemunByIdiCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeRemunByIdiCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiQuery query)
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
			this.InitQuery(query as esParamedicFeeRemunByIdiQuery);
		}
		#endregion
			
		virtual public ParamedicFeeRemunByIdi DetachEntity(ParamedicFeeRemunByIdi entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeRemunByIdi;
		}
		
		virtual public ParamedicFeeRemunByIdi AttachEntity(ParamedicFeeRemunByIdi entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeRemunByIdi;
		}
		
		virtual public void Combine(ParamedicFeeRemunByIdiCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeRemunByIdi this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeRemunByIdi;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeRemunByIdi);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdi : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeRemunByIdiQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeRemunByIdi()
		{
		}
	
		public esParamedicFeeRemunByIdi(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 remunID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 remunID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 remunID)
		{
			esParamedicFeeRemunByIdiQuery query = this.GetDynamicQuery();
			query.Where(query.RemunID==remunID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 remunID)
		{
			esParameters parms = new esParameters();
			parms.Add("RemunID",remunID);
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
						case "RemunID": this.str.RemunID = (string)value; break;
						case "RemunNo": this.str.RemunNo = (string)value; break;
						case "FundAllocProcedure": this.str.FundAllocProcedure = (string)value; break;
						case "FundAllocPosition": this.str.FundAllocPosition = (string)value; break;
						case "FundAllocInsetif": this.str.FundAllocInsetif = (string)value; break;
						case "KursPosition": this.str.KursPosition = (string)value; break;
						case "KursInsentif": this.str.KursInsentif = (string)value; break;
						case "AdjustmentFactor": this.str.AdjustmentFactor = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DateStart": this.str.DateStart = (string)value; break;
						case "DateEnd": this.str.DateEnd = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RemunID":
						
							if (value == null || value is System.Int32)
								this.RemunID = (System.Int32?)value;
							break;
						case "FundAllocProcedure":
						
							if (value == null || value is System.Decimal)
								this.FundAllocProcedure = (System.Decimal?)value;
							break;
						case "FundAllocPosition":
						
							if (value == null || value is System.Decimal)
								this.FundAllocPosition = (System.Decimal?)value;
							break;
						case "FundAllocInsetif":
						
							if (value == null || value is System.Decimal)
								this.FundAllocInsetif = (System.Decimal?)value;
							break;
						case "KursPosition":
						
							if (value == null || value is System.Decimal)
								this.KursPosition = (System.Decimal?)value;
							break;
						case "KursInsentif":
						
							if (value == null || value is System.Decimal)
								this.KursInsentif = (System.Decimal?)value;
							break;
						case "AdjustmentFactor":
						
							if (value == null || value is System.Decimal)
								this.AdjustmentFactor = (System.Decimal?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":
						
							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "DateStart":
						
							if (value == null || value is System.DateTime)
								this.DateStart = (System.DateTime?)value;
							break;
						case "DateEnd":
						
							if (value == null || value is System.DateTime)
								this.DateEnd = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeRemunByIdi.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.RemunNo
		/// </summary>
		virtual public System.String RemunNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.FundAllocProcedure
		/// </summary>
		virtual public System.Decimal? FundAllocProcedure
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocProcedure);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocProcedure, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.FundAllocPosition
		/// </summary>
		virtual public System.Decimal? FundAllocPosition
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocPosition);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocPosition, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.FundAllocInsetif
		/// </summary>
		virtual public System.Decimal? FundAllocInsetif
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocInsetif);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocInsetif, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.KursPosition
		/// </summary>
		virtual public System.Decimal? KursPosition
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.KursPosition);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.KursPosition, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.KursInsentif
		/// </summary>
		virtual public System.Decimal? KursInsentif
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.KursInsentif);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.KursInsentif, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.AdjustmentFactor
		/// </summary>
		virtual public System.Decimal? AdjustmentFactor
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.AdjustmentFactor);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiMetadata.ColumnNames.AdjustmentFactor, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeRemunByIdiMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeRemunByIdiMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeRemunByIdiMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeRemunByIdiMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.DateStart
		/// </summary>
		virtual public System.DateTime? DateStart
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.DateStart);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.DateStart, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdi.DateEnd
		/// </summary>
		virtual public System.DateTime? DateEnd
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.DateEnd);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiMetadata.ColumnNames.DateEnd, value);
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
			public esStrings(esParamedicFeeRemunByIdi entity)
			{
				this.entity = entity;
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
			public System.String RemunNo
			{
				get
				{
					System.String data = entity.RemunNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunNo = null;
					else entity.RemunNo = Convert.ToString(value);
				}
			}
			public System.String FundAllocProcedure
			{
				get
				{
					System.Decimal? data = entity.FundAllocProcedure;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAllocProcedure = null;
					else entity.FundAllocProcedure = Convert.ToDecimal(value);
				}
			}
			public System.String FundAllocPosition
			{
				get
				{
					System.Decimal? data = entity.FundAllocPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAllocPosition = null;
					else entity.FundAllocPosition = Convert.ToDecimal(value);
				}
			}
			public System.String FundAllocInsetif
			{
				get
				{
					System.Decimal? data = entity.FundAllocInsetif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAllocInsetif = null;
					else entity.FundAllocInsetif = Convert.ToDecimal(value);
				}
			}
			public System.String KursPosition
			{
				get
				{
					System.Decimal? data = entity.KursPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KursPosition = null;
					else entity.KursPosition = Convert.ToDecimal(value);
				}
			}
			public System.String KursInsentif
			{
				get
				{
					System.Decimal? data = entity.KursInsentif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KursInsentif = null;
					else entity.KursInsentif = Convert.ToDecimal(value);
				}
			}
			public System.String AdjustmentFactor
			{
				get
				{
					System.Decimal? data = entity.AdjustmentFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustmentFactor = null;
					else entity.AdjustmentFactor = Convert.ToDecimal(value);
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
			public System.String DateStart
			{
				get
				{
					System.DateTime? data = entity.DateStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateStart = null;
					else entity.DateStart = Convert.ToDateTime(value);
				}
			}
			public System.String DateEnd
			{
				get
				{
					System.DateTime? data = entity.DateEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateEnd = null;
					else entity.DateEnd = Convert.ToDateTime(value);
				}
			}
			private esParamedicFeeRemunByIdi entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiQuery query)
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
				throw new Exception("esParamedicFeeRemunByIdi can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeRemunByIdi : esParamedicFeeRemunByIdi
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RemunNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunNo, esSystemType.String);
			}
		} 
			
		public esQueryItem FundAllocProcedure
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocProcedure, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FundAllocPosition
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocPosition, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FundAllocInsetif
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocInsetif, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem KursPosition
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.KursPosition, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem KursInsentif
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.KursInsentif, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AdjustmentFactor
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.AdjustmentFactor, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem DateStart
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.DateStart, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem DateEnd
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiMetadata.ColumnNames.DateEnd, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeRemunByIdiCollection")]
	public partial class ParamedicFeeRemunByIdiCollection : esParamedicFeeRemunByIdiCollection, IEnumerable< ParamedicFeeRemunByIdi>
	{
		public ParamedicFeeRemunByIdiCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeRemunByIdi>(ParamedicFeeRemunByIdiCollection coll)
		{
			List< ParamedicFeeRemunByIdi> list = new List< ParamedicFeeRemunByIdi>();
			
			foreach (ParamedicFeeRemunByIdi emp in coll)
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
				return  ParamedicFeeRemunByIdiMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeRemunByIdi(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeRemunByIdi();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiQuery();
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
		public bool Load(ParamedicFeeRemunByIdiQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeRemunByIdi AddNew()
		{
			ParamedicFeeRemunByIdi entity = base.AddNewEntity() as ParamedicFeeRemunByIdi;
			
			return entity;		
		}
		public ParamedicFeeRemunByIdi FindByPrimaryKey(Int32 remunID)
		{
			return base.FindByPrimaryKey(remunID) as ParamedicFeeRemunByIdi;
		}

		#region IEnumerable< ParamedicFeeRemunByIdi> Members

		IEnumerator< ParamedicFeeRemunByIdi> IEnumerable< ParamedicFeeRemunByIdi>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeRemunByIdi;
			}
		}

		#endregion
		
		private ParamedicFeeRemunByIdiQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeRemunByIdi' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeRemunByIdi ({RemunID})")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdi : esParamedicFeeRemunByIdi
	{
		public ParamedicFeeRemunByIdi()
		{
		}	
	
		public ParamedicFeeRemunByIdi(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeRemunByIdiQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiQuery();
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
		public bool Load(ParamedicFeeRemunByIdiQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeRemunByIdiQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiQuery : esParamedicFeeRemunByIdiQuery
	{
		public ParamedicFeeRemunByIdiQuery()
		{

		}		
		
		public ParamedicFeeRemunByIdiQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeRemunByIdiQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeRemunByIdiMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeRemunByIdiMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.RemunID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.RemunNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.RemunNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocProcedure, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.FundAllocProcedure;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocPosition, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.FundAllocPosition;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.FundAllocInsetif, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.FundAllocInsetif;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.KursPosition, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.KursPosition;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.KursInsentif, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.KursInsentif;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.AdjustmentFactor, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.AdjustmentFactor;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.IsApproved, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.IsApproved;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.ApprovedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.CreateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.DateStart, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.DateStart;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiMetadata.ColumnNames.DateEnd, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiMetadata.PropertyNames.DateEnd;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeRemunByIdiMetadata Meta()
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
			public const string RemunID = "RemunID";
			public const string RemunNo = "RemunNo";
			public const string FundAllocProcedure = "FundAllocProcedure";
			public const string FundAllocPosition = "FundAllocPosition";
			public const string FundAllocInsetif = "FundAllocInsetif";
			public const string KursPosition = "KursPosition";
			public const string KursInsentif = "KursInsentif";
			public const string AdjustmentFactor = "AdjustmentFactor";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DateStart = "DateStart";
			public const string DateEnd = "DateEnd";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunID = "RemunID";
			public const string RemunNo = "RemunNo";
			public const string FundAllocProcedure = "FundAllocProcedure";
			public const string FundAllocPosition = "FundAllocPosition";
			public const string FundAllocInsetif = "FundAllocInsetif";
			public const string KursPosition = "KursPosition";
			public const string KursInsentif = "KursInsentif";
			public const string AdjustmentFactor = "AdjustmentFactor";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DateStart = "DateStart";
			public const string DateEnd = "DateEnd";
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
			lock (typeof(ParamedicFeeRemunByIdiMetadata))
			{
				if(ParamedicFeeRemunByIdiMetadata.mapDelegates == null)
				{
					ParamedicFeeRemunByIdiMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeRemunByIdiMetadata.meta == null)
				{
					ParamedicFeeRemunByIdiMetadata.meta = new ParamedicFeeRemunByIdiMetadata();
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
				
				meta.AddTypeMap("RemunID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RemunNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FundAllocProcedure", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FundAllocPosition", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FundAllocInsetif", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("KursPosition", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("KursInsentif", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AdjustmentFactor", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateStart", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DateEnd", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ParamedicFeeRemunByIdi";
				meta.Destination = "ParamedicFeeRemunByIdi";
				meta.spInsert = "proc_ParamedicFeeRemunByIdiInsert";				
				meta.spUpdate = "proc_ParamedicFeeRemunByIdiUpdate";		
				meta.spDelete = "proc_ParamedicFeeRemunByIdiDelete";
				meta.spLoadAll = "proc_ParamedicFeeRemunByIdiLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeRemunByIdiLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeRemunByIdiMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
