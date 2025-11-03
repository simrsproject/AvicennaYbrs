/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/1/2021 4:15:20 PM
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
	abstract public class esItemLaboratoryCollection : esEntityCollectionWAuditLog
	{
		public esItemLaboratoryCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ItemLaboratoryCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esItemLaboratoryQuery query)
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
			this.InitQuery(query as esItemLaboratoryQuery);
		}
		#endregion
			
		virtual public ItemLaboratory DetachEntity(ItemLaboratory entity)
		{
			return base.DetachEntity(entity) as ItemLaboratory;
		}
		
		virtual public ItemLaboratory AttachEntity(ItemLaboratory entity)
		{
			return base.AttachEntity(entity) as ItemLaboratory;
		}
		
		virtual public void Combine(ItemLaboratoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemLaboratory this[int index]
		{
			get
			{
				return base[index] as ItemLaboratory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemLaboratory);
		}
	}

	[Serializable]
	abstract public class esItemLaboratory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemLaboratoryQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esItemLaboratory()
		{
		}
	
		public esItemLaboratory(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String itemID)
		{
			esItemLaboratoryQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ReportRLID": this.str.ReportRLID = (string)value; break;
						case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
						case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
						case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
						case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
						case "IsAssetUtilization": this.str.IsAssetUtilization = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsDisplayInOrderList": this.str.IsDisplayInOrderList = (string)value; break;
						case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
						case "SRExaminationClass": this.str.SRExaminationClass = (string)value; break;
						case "SRLaboratoryUnit": this.str.SRLaboratoryUnit = (string)value; break;
						case "LevelNo": this.str.LevelNo = (string)value; break;
						case "DisplaySequence": this.str.DisplaySequence = (string)value; break;
						case "IsConfidential": this.str.IsConfidential = (string)value; break;
						case "IsResultOnSepataredPage": this.str.IsResultOnSepataredPage = (string)value; break;
						case "IsCitoFromStandardReference": this.str.IsCitoFromStandardReference = (string)value; break;
						case "WaitingTimeForResults": this.str.WaitingTimeForResults = (string)value; break;
						case "SRIntervalTime": this.str.SRIntervalTime = (string)value; break;
						case "SRSpecimenType": this.str.SRSpecimenType = (string)value; break;
						case "IsCulture": this.str.IsCulture = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsAdminCalculation":
						
							if (value == null || value is System.Boolean)
								this.IsAdminCalculation = (System.Boolean?)value;
							break;
						case "IsAllowVariable":
						
							if (value == null || value is System.Boolean)
								this.IsAllowVariable = (System.Boolean?)value;
							break;
						case "IsAllowCito":
						
							if (value == null || value is System.Boolean)
								this.IsAllowCito = (System.Boolean?)value;
							break;
						case "IsAllowDiscount":
						
							if (value == null || value is System.Boolean)
								this.IsAllowDiscount = (System.Boolean?)value;
							break;
						case "IsAssetUtilization":
						
							if (value == null || value is System.Boolean)
								this.IsAssetUtilization = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsDisplayInOrderList":
						
							if (value == null || value is System.Boolean)
								this.IsDisplayInOrderList = (System.Boolean?)value;
							break;
						case "RlMasterReportItemID":
						
							if (value == null || value is System.Int32)
								this.RlMasterReportItemID = (System.Int32?)value;
							break;
						case "LevelNo":
						
							if (value == null || value is System.Int32)
								this.LevelNo = (System.Int32?)value;
							break;
						case "DisplaySequence":
						
							if (value == null || value is System.Int32)
								this.DisplaySequence = (System.Int32?)value;
							break;
						case "IsConfidential":
						
							if (value == null || value is System.Boolean)
								this.IsConfidential = (System.Boolean?)value;
							break;
						case "IsResultOnSepataredPage":
						
							if (value == null || value is System.Boolean)
								this.IsResultOnSepataredPage = (System.Boolean?)value;
							break;
						case "IsCitoFromStandardReference":
						
							if (value == null || value is System.Boolean)
								this.IsCitoFromStandardReference = (System.Boolean?)value;
							break;
						case "WaitingTimeForResults":
						
							if (value == null || value is System.Int16)
								this.WaitingTimeForResults = (System.Int16?)value;
							break;
						case "IsCulture":
						
							if (value == null || value is System.Boolean)
								this.IsCulture = (System.Boolean?)value;
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
		/// Maps to ItemLaboratory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.ReportRLID
		/// </summary>
		virtual public System.String ReportRLID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.ReportRLID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.ReportRLID, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsAdminCalculation
		/// </summary>
		virtual public System.Boolean? IsAdminCalculation
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAdminCalculation);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAdminCalculation, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsAllowVariable
		/// </summary>
		virtual public System.Boolean? IsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAllowVariable);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAllowVariable, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsAllowCito
		/// </summary>
		virtual public System.Boolean? IsAllowCito
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAllowCito);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAllowCito, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsAllowDiscount
		/// </summary>
		virtual public System.Boolean? IsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAllowDiscount);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAllowDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsAssetUtilization
		/// </summary>
		virtual public System.Boolean? IsAssetUtilization
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAssetUtilization);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsAssetUtilization, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemLaboratoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemLaboratoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsDisplayInOrderList
		/// </summary>
		virtual public System.Boolean? IsDisplayInOrderList
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsDisplayInOrderList);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsDisplayInOrderList, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(ItemLaboratoryMetadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(ItemLaboratoryMetadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.SRExaminationClass
		/// </summary>
		virtual public System.String SRExaminationClass
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.SRExaminationClass);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.SRExaminationClass, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.SRLaboratoryUnit
		/// </summary>
		virtual public System.String SRLaboratoryUnit
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.SRLaboratoryUnit);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.SRLaboratoryUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.LevelNo
		/// </summary>
		virtual public System.Int32? LevelNo
		{
			get
			{
				return base.GetSystemInt32(ItemLaboratoryMetadata.ColumnNames.LevelNo);
			}
			
			set
			{
				base.SetSystemInt32(ItemLaboratoryMetadata.ColumnNames.LevelNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.DisplaySequence
		/// </summary>
		virtual public System.Int32? DisplaySequence
		{
			get
			{
				return base.GetSystemInt32(ItemLaboratoryMetadata.ColumnNames.DisplaySequence);
			}
			
			set
			{
				base.SetSystemInt32(ItemLaboratoryMetadata.ColumnNames.DisplaySequence, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsConfidential
		/// </summary>
		virtual public System.Boolean? IsConfidential
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsConfidential);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsConfidential, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsResultOnSepataredPage
		/// </summary>
		virtual public System.Boolean? IsResultOnSepataredPage
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsResultOnSepataredPage);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsResultOnSepataredPage, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsCitoFromStandardReference
		/// </summary>
		virtual public System.Boolean? IsCitoFromStandardReference
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsCitoFromStandardReference);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsCitoFromStandardReference, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.WaitingTimeForResults
		/// </summary>
		virtual public System.Int16? WaitingTimeForResults
		{
			get
			{
				return base.GetSystemInt16(ItemLaboratoryMetadata.ColumnNames.WaitingTimeForResults);
			}
			
			set
			{
				base.SetSystemInt16(ItemLaboratoryMetadata.ColumnNames.WaitingTimeForResults, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.SRIntervalTime
		/// </summary>
		virtual public System.String SRIntervalTime
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.SRIntervalTime);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.SRIntervalTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.SRSpecimenType
		/// </summary>
		virtual public System.String SRSpecimenType
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryMetadata.ColumnNames.SRSpecimenType);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryMetadata.ColumnNames.SRSpecimenType, value);
			}
		}
		/// <summary>
		/// Maps to ItemLaboratory.IsCulture
		/// </summary>
		virtual public System.Boolean? IsCulture
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsCulture);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryMetadata.ColumnNames.IsCulture, value);
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
			public esStrings(esItemLaboratory entity)
			{
				this.entity = entity;
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
			public System.String ReportRLID
			{
				get
				{
					System.String data = entity.ReportRLID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportRLID = null;
					else entity.ReportRLID = Convert.ToString(value);
				}
			}
			public System.String IsAdminCalculation
			{
				get
				{
					System.Boolean? data = entity.IsAdminCalculation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdminCalculation = null;
					else entity.IsAdminCalculation = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowVariable
			{
				get
				{
					System.Boolean? data = entity.IsAllowVariable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowVariable = null;
					else entity.IsAllowVariable = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowCito
			{
				get
				{
					System.Boolean? data = entity.IsAllowCito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowCito = null;
					else entity.IsAllowCito = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowDiscount
			{
				get
				{
					System.Boolean? data = entity.IsAllowDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
					else entity.IsAllowDiscount = Convert.ToBoolean(value);
				}
			}
			public System.String IsAssetUtilization
			{
				get
				{
					System.Boolean? data = entity.IsAssetUtilization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAssetUtilization = null;
					else entity.IsAssetUtilization = Convert.ToBoolean(value);
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
			public System.String IsDisplayInOrderList
			{
				get
				{
					System.Boolean? data = entity.IsDisplayInOrderList;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisplayInOrderList = null;
					else entity.IsDisplayInOrderList = Convert.ToBoolean(value);
				}
			}
			public System.String RlMasterReportItemID
			{
				get
				{
					System.Int32? data = entity.RlMasterReportItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
					else entity.RlMasterReportItemID = Convert.ToInt32(value);
				}
			}
			public System.String SRExaminationClass
			{
				get
				{
					System.String data = entity.SRExaminationClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRExaminationClass = null;
					else entity.SRExaminationClass = Convert.ToString(value);
				}
			}
			public System.String SRLaboratoryUnit
			{
				get
				{
					System.String data = entity.SRLaboratoryUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaboratoryUnit = null;
					else entity.SRLaboratoryUnit = Convert.ToString(value);
				}
			}
			public System.String LevelNo
			{
				get
				{
					System.Int32? data = entity.LevelNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LevelNo = null;
					else entity.LevelNo = Convert.ToInt32(value);
				}
			}
			public System.String DisplaySequence
			{
				get
				{
					System.Int32? data = entity.DisplaySequence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisplaySequence = null;
					else entity.DisplaySequence = Convert.ToInt32(value);
				}
			}
			public System.String IsConfidential
			{
				get
				{
					System.Boolean? data = entity.IsConfidential;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConfidential = null;
					else entity.IsConfidential = Convert.ToBoolean(value);
				}
			}
			public System.String IsResultOnSepataredPage
			{
				get
				{
					System.Boolean? data = entity.IsResultOnSepataredPage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsResultOnSepataredPage = null;
					else entity.IsResultOnSepataredPage = Convert.ToBoolean(value);
				}
			}
			public System.String IsCitoFromStandardReference
			{
				get
				{
					System.Boolean? data = entity.IsCitoFromStandardReference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCitoFromStandardReference = null;
					else entity.IsCitoFromStandardReference = Convert.ToBoolean(value);
				}
			}
			public System.String WaitingTimeForResults
			{
				get
				{
					System.Int16? data = entity.WaitingTimeForResults;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WaitingTimeForResults = null;
					else entity.WaitingTimeForResults = Convert.ToInt16(value);
				}
			}
			public System.String SRIntervalTime
			{
				get
				{
					System.String data = entity.SRIntervalTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIntervalTime = null;
					else entity.SRIntervalTime = Convert.ToString(value);
				}
			}
			public System.String SRSpecimenType
			{
				get
				{
					System.String data = entity.SRSpecimenType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSpecimenType = null;
					else entity.SRSpecimenType = Convert.ToString(value);
				}
			}
			public System.String IsCulture
			{
				get
				{
					System.Boolean? data = entity.IsCulture;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCulture = null;
					else entity.IsCulture = Convert.ToBoolean(value);
				}
			}
			private esItemLaboratory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemLaboratoryQuery query)
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
				throw new Exception("esItemLaboratory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemLaboratory : esItemLaboratory
	{	
	}

	[Serializable]
	abstract public class esItemLaboratoryQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ItemLaboratoryMetadata.Meta();
			}
		}	
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReportRLID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.ReportRLID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAdminCalculation
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAllowCito
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAssetUtilization
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDisplayInOrderList
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsDisplayInOrderList, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRExaminationClass
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.SRExaminationClass, esSystemType.String);
			}
		} 
			
		public esQueryItem SRLaboratoryUnit
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.SRLaboratoryUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem LevelNo
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.LevelNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DisplaySequence
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.DisplaySequence, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsConfidential
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsConfidential, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsResultOnSepataredPage
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsResultOnSepataredPage, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsCitoFromStandardReference
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsCitoFromStandardReference, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem WaitingTimeForResults
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.WaitingTimeForResults, esSystemType.Int16);
			}
		} 
			
		public esQueryItem SRIntervalTime
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.SRIntervalTime, esSystemType.String);
			}
		} 
			
		public esQueryItem SRSpecimenType
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.SRSpecimenType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsCulture
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryMetadata.ColumnNames.IsCulture, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemLaboratoryCollection")]
	public partial class ItemLaboratoryCollection : esItemLaboratoryCollection, IEnumerable< ItemLaboratory>
	{
		public ItemLaboratoryCollection()
		{

		}	
		
		public static implicit operator List< ItemLaboratory>(ItemLaboratoryCollection coll)
		{
			List< ItemLaboratory> list = new List< ItemLaboratory>();
			
			foreach (ItemLaboratory emp in coll)
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
				return  ItemLaboratoryMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLaboratoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemLaboratory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemLaboratory();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ItemLaboratoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLaboratoryQuery();
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
		public bool Load(ItemLaboratoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemLaboratory AddNew()
		{
			ItemLaboratory entity = base.AddNewEntity() as ItemLaboratory;
			
			return entity;		
		}
		public ItemLaboratory FindByPrimaryKey(String itemID)
		{
			return base.FindByPrimaryKey(itemID) as ItemLaboratory;
		}

		#region IEnumerable< ItemLaboratory> Members

		IEnumerator< ItemLaboratory> IEnumerable< ItemLaboratory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemLaboratory;
			}
		}

		#endregion
		
		private ItemLaboratoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemLaboratory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemLaboratory ({ItemID})")]
	[Serializable]
	public partial class ItemLaboratory : esItemLaboratory
	{
		public ItemLaboratory()
		{
		}	
	
		public ItemLaboratory(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemLaboratoryMetadata.Meta();
			}
		}	
	
		override protected esItemLaboratoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLaboratoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ItemLaboratoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLaboratoryQuery();
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
		public bool Load(ItemLaboratoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ItemLaboratoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemLaboratoryQuery : esItemLaboratoryQuery
	{
		public ItemLaboratoryQuery()
		{

		}		
		
		public ItemLaboratoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ItemLaboratoryQuery";
        }
	}

	[Serializable]
	public partial class ItemLaboratoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemLaboratoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.ReportRLID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.ReportRLID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsAdminCalculation, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsAdminCalculation;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsAllowVariable, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsAllowVariable;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsAllowCito, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsAllowCito;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsAllowDiscount, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsAllowDiscount;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsAssetUtilization, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsAssetUtilization;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsDisplayInOrderList, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsDisplayInOrderList;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.RlMasterReportItemID, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.RlMasterReportItemID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.SRExaminationClass, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.SRExaminationClass;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.SRLaboratoryUnit, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.SRLaboratoryUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.LevelNo, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.LevelNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.DisplaySequence, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.DisplaySequence;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsConfidential, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsConfidential;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsResultOnSepataredPage, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsResultOnSepataredPage;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsCitoFromStandardReference, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsCitoFromStandardReference;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.WaitingTimeForResults, 18, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.WaitingTimeForResults;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.SRIntervalTime, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.SRIntervalTime;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.SRSpecimenType, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.SRSpecimenType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemLaboratoryMetadata.ColumnNames.IsCulture, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryMetadata.PropertyNames.IsCulture;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ItemLaboratoryMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ReportRLID = "ReportRLID";
			public const string IsAdminCalculation = "IsAdminCalculation";
			public const string IsAllowVariable = "IsAllowVariable";
			public const string IsAllowCito = "IsAllowCito";
			public const string IsAllowDiscount = "IsAllowDiscount";
			public const string IsAssetUtilization = "IsAssetUtilization";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsDisplayInOrderList = "IsDisplayInOrderList";
			public const string RlMasterReportItemID = "RlMasterReportItemID";
			public const string SRExaminationClass = "SRExaminationClass";
			public const string SRLaboratoryUnit = "SRLaboratoryUnit";
			public const string LevelNo = "LevelNo";
			public const string DisplaySequence = "DisplaySequence";
			public const string IsConfidential = "IsConfidential";
			public const string IsResultOnSepataredPage = "IsResultOnSepataredPage";
			public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
			public const string WaitingTimeForResults = "WaitingTimeForResults";
			public const string SRIntervalTime = "SRIntervalTime";
			public const string SRSpecimenType = "SRSpecimenType";
			public const string IsCulture = "IsCulture";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ItemID = "ItemID";
			public const string ReportRLID = "ReportRLID";
			public const string IsAdminCalculation = "IsAdminCalculation";
			public const string IsAllowVariable = "IsAllowVariable";
			public const string IsAllowCito = "IsAllowCito";
			public const string IsAllowDiscount = "IsAllowDiscount";
			public const string IsAssetUtilization = "IsAssetUtilization";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsDisplayInOrderList = "IsDisplayInOrderList";
			public const string RlMasterReportItemID = "RlMasterReportItemID";
			public const string SRExaminationClass = "SRExaminationClass";
			public const string SRLaboratoryUnit = "SRLaboratoryUnit";
			public const string LevelNo = "LevelNo";
			public const string DisplaySequence = "DisplaySequence";
			public const string IsConfidential = "IsConfidential";
			public const string IsResultOnSepataredPage = "IsResultOnSepataredPage";
			public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
			public const string WaitingTimeForResults = "WaitingTimeForResults";
			public const string SRIntervalTime = "SRIntervalTime";
			public const string SRSpecimenType = "SRSpecimenType";
			public const string IsCulture = "IsCulture";
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
			lock (typeof(ItemLaboratoryMetadata))
			{
				if(ItemLaboratoryMetadata.mapDelegates == null)
				{
					ItemLaboratoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemLaboratoryMetadata.meta == null)
				{
					ItemLaboratoryMetadata.meta = new ItemLaboratoryMetadata();
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
				
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportRLID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAssetUtilization", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDisplayInOrderList", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRExaminationClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaboratoryUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LevelNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DisplaySequence", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsConfidential", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsResultOnSepataredPage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("WaitingTimeForResults", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SRIntervalTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSpecimenType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCulture", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "ItemLaboratory";
				meta.Destination = "ItemLaboratory";
				meta.spInsert = "proc_ItemLaboratoryInsert";				
				meta.spUpdate = "proc_ItemLaboratoryUpdate";		
				meta.spDelete = "proc_ItemLaboratoryDelete";
				meta.spLoadAll = "proc_ItemLaboratoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemLaboratoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemLaboratoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
