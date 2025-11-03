/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/27/2023 4:06:25 PM
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
	abstract public class esVitalSignCollection : esEntityCollectionWAuditLog
	{
		public esVitalSignCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "VitalSignCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVitalSignQuery query)
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
			this.InitQuery(query as esVitalSignQuery);
		}
		#endregion
			
		virtual public VitalSign DetachEntity(VitalSign entity)
		{
			return base.DetachEntity(entity) as VitalSign;
		}
		
		virtual public VitalSign AttachEntity(VitalSign entity)
		{
			return base.AttachEntity(entity) as VitalSign;
		}
		
		virtual public void Combine(VitalSignCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VitalSign this[int index]
		{
			get
			{
				return base[index] as VitalSign;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VitalSign);
		}
	}

	[Serializable]
	abstract public class esVitalSign : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVitalSignQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVitalSign()
		{
		}
	
		public esVitalSign(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String vitalSignID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(vitalSignID);
			else
				return LoadByPrimaryKeyStoredProcedure(vitalSignID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String vitalSignID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(vitalSignID);
			else
				return LoadByPrimaryKeyStoredProcedure(vitalSignID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String vitalSignID)
		{
			esVitalSignQuery query = this.GetDynamicQuery();
			query.Where(query.VitalSignID == vitalSignID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String vitalSignID)
		{
			esParameters parms = new esParameters();
			parms.Add("VitalSignID",vitalSignID);
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
						case "VitalSignID": this.str.VitalSignID = (string)value; break;
						case "VitalSignName": this.str.VitalSignName = (string)value; break;
						case "VitalSignInitial": this.str.VitalSignInitial = (string)value; break;
						case "SRVitalSignGroup": this.str.SRVitalSignGroup = (string)value; break;
						case "RowIndexInGroup": this.str.RowIndexInGroup = (string)value; break;
						case "ValueType": this.str.ValueType = (string)value; break;
						case "StandardReferenceID": this.str.StandardReferenceID = (string)value; break;
						case "EntryMask": this.str.EntryMask = (string)value; break;
						case "VitalSignUnit": this.str.VitalSignUnit = (string)value; break;
						case "NumType": this.str.NumType = (string)value; break;
						case "NumDecimalDigits": this.str.NumDecimalDigits = (string)value; break;
						case "NumMinValue": this.str.NumMinValue = (string)value; break;
						case "NumMaxValue": this.str.NumMaxValue = (string)value; break;
						case "NumMaxLength": this.str.NumMaxLength = (string)value; break;
						case "IsMonitoring": this.str.IsMonitoring = (string)value; break;
						case "IsChart": this.str.IsChart = (string)value; break;
						case "ChartColor": this.str.ChartColor = (string)value; break;
						case "ChartMinValue": this.str.ChartMinValue = (string)value; break;
						case "ChartMaxValue": this.str.ChartMaxValue = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "QuestionID": this.str.QuestionID = (string)value; break;
						case "ParentVitalSignID": this.str.ParentVitalSignID = (string)value; break;
						case "ChartYAxisStep": this.str.ChartYAxisStep = (string)value; break;
						case "RowIndex": this.str.RowIndex = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RowIndexInGroup":
						
							if (value == null || value is System.Int32)
								this.RowIndexInGroup = (System.Int32?)value;
							break;
						case "NumDecimalDigits":
						
							if (value == null || value is System.Int32)
								this.NumDecimalDigits = (System.Int32?)value;
							break;
						case "NumMinValue":
						
							if (value == null || value is System.Int32)
								this.NumMinValue = (System.Int32?)value;
							break;
						case "NumMaxValue":
						
							if (value == null || value is System.Int32)
								this.NumMaxValue = (System.Int32?)value;
							break;
						case "NumMaxLength":
						
							if (value == null || value is System.Int32)
								this.NumMaxLength = (System.Int32?)value;
							break;
						case "IsMonitoring":
						
							if (value == null || value is System.Boolean)
								this.IsMonitoring = (System.Boolean?)value;
							break;
						case "IsChart":
						
							if (value == null || value is System.Boolean)
								this.IsChart = (System.Boolean?)value;
							break;
						case "ChartColor":
						
							if (value == null || value is System.Int32)
								this.ChartColor = (System.Int32?)value;
							break;
						case "ChartMinValue":
						
							if (value == null || value is System.Int32)
								this.ChartMinValue = (System.Int32?)value;
							break;
						case "ChartMaxValue":
						
							if (value == null || value is System.Int32)
								this.ChartMaxValue = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ChartYAxisStep":
						
							if (value == null || value is System.Decimal)
								this.ChartYAxisStep = (System.Decimal?)value;
							break;
						case "RowIndex":
						
							if (value == null || value is System.Int32)
								this.RowIndex = (System.Int32?)value;
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
		/// Maps to VitalSign.VitalSignID
		/// </summary>
		virtual public System.String VitalSignID
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.VitalSignID);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.VitalSignID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.VitalSignName
		/// </summary>
		virtual public System.String VitalSignName
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.VitalSignName);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.VitalSignName, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.VitalSignInitial
		/// </summary>
		virtual public System.String VitalSignInitial
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.VitalSignInitial);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.VitalSignInitial, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.SRVitalSignGroup
		/// </summary>
		virtual public System.String SRVitalSignGroup
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.SRVitalSignGroup);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.SRVitalSignGroup, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.RowIndexInGroup
		/// </summary>
		virtual public System.Int32? RowIndexInGroup
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.RowIndexInGroup);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.RowIndexInGroup, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.ValueType
		/// </summary>
		virtual public System.String ValueType
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.ValueType);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.ValueType, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.StandardReferenceID
		/// </summary>
		virtual public System.String StandardReferenceID
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.StandardReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.EntryMask
		/// </summary>
		virtual public System.String EntryMask
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.EntryMask);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.EntryMask, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.VitalSignUnit
		/// </summary>
		virtual public System.String VitalSignUnit
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.VitalSignUnit);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.VitalSignUnit, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.NumType
		/// </summary>
		virtual public System.String NumType
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.NumType);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.NumType, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.NumDecimalDigits
		/// </summary>
		virtual public System.Int32? NumDecimalDigits
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.NumDecimalDigits);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.NumDecimalDigits, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.NumMinValue
		/// </summary>
		virtual public System.Int32? NumMinValue
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.NumMinValue);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.NumMinValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.NumMaxValue
		/// </summary>
		virtual public System.Int32? NumMaxValue
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.NumMaxValue);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.NumMaxValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.NumMaxLength
		/// </summary>
		virtual public System.Int32? NumMaxLength
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.NumMaxLength);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.NumMaxLength, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.IsMonitoring
		/// </summary>
		virtual public System.Boolean? IsMonitoring
		{
			get
			{
				return base.GetSystemBoolean(VitalSignMetadata.ColumnNames.IsMonitoring);
			}
			
			set
			{
				base.SetSystemBoolean(VitalSignMetadata.ColumnNames.IsMonitoring, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.IsChart
		/// </summary>
		virtual public System.Boolean? IsChart
		{
			get
			{
				return base.GetSystemBoolean(VitalSignMetadata.ColumnNames.IsChart);
			}
			
			set
			{
				base.SetSystemBoolean(VitalSignMetadata.ColumnNames.IsChart, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.ChartColor
		/// </summary>
		virtual public System.Int32? ChartColor
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.ChartColor);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.ChartColor, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.ChartMinValue
		/// </summary>
		virtual public System.Int32? ChartMinValue
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.ChartMinValue);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.ChartMinValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.ChartMaxValue
		/// </summary>
		virtual public System.Int32? ChartMaxValue
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.ChartMaxValue);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.ChartMaxValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VitalSignMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VitalSignMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.QuestionID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.ParentVitalSignID
		/// </summary>
		virtual public System.String ParentVitalSignID
		{
			get
			{
				return base.GetSystemString(VitalSignMetadata.ColumnNames.ParentVitalSignID);
			}
			
			set
			{
				base.SetSystemString(VitalSignMetadata.ColumnNames.ParentVitalSignID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.ChartYAxisStep
		/// </summary>
		virtual public System.Decimal? ChartYAxisStep
		{
			get
			{
				return base.GetSystemDecimal(VitalSignMetadata.ColumnNames.ChartYAxisStep);
			}
			
			set
			{
				base.SetSystemDecimal(VitalSignMetadata.ColumnNames.ChartYAxisStep, value);
			}
		}
		/// <summary>
		/// Maps to VitalSign.RowIndex
		/// </summary>
		virtual public System.Int32? RowIndex
		{
			get
			{
				return base.GetSystemInt32(VitalSignMetadata.ColumnNames.RowIndex);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignMetadata.ColumnNames.RowIndex, value);
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
			public esStrings(esVitalSign entity)
			{
				this.entity = entity;
			}
			public System.String VitalSignID
			{
				get
				{
					System.String data = entity.VitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignID = null;
					else entity.VitalSignID = Convert.ToString(value);
				}
			}
			public System.String VitalSignName
			{
				get
				{
					System.String data = entity.VitalSignName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignName = null;
					else entity.VitalSignName = Convert.ToString(value);
				}
			}
			public System.String VitalSignInitial
			{
				get
				{
					System.String data = entity.VitalSignInitial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignInitial = null;
					else entity.VitalSignInitial = Convert.ToString(value);
				}
			}
			public System.String SRVitalSignGroup
			{
				get
				{
					System.String data = entity.SRVitalSignGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVitalSignGroup = null;
					else entity.SRVitalSignGroup = Convert.ToString(value);
				}
			}
			public System.String RowIndexInGroup
			{
				get
				{
					System.Int32? data = entity.RowIndexInGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RowIndexInGroup = null;
					else entity.RowIndexInGroup = Convert.ToInt32(value);
				}
			}
			public System.String ValueType
			{
				get
				{
					System.String data = entity.ValueType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueType = null;
					else entity.ValueType = Convert.ToString(value);
				}
			}
			public System.String StandardReferenceID
			{
				get
				{
					System.String data = entity.StandardReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardReferenceID = null;
					else entity.StandardReferenceID = Convert.ToString(value);
				}
			}
			public System.String EntryMask
			{
				get
				{
					System.String data = entity.EntryMask;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EntryMask = null;
					else entity.EntryMask = Convert.ToString(value);
				}
			}
			public System.String VitalSignUnit
			{
				get
				{
					System.String data = entity.VitalSignUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignUnit = null;
					else entity.VitalSignUnit = Convert.ToString(value);
				}
			}
			public System.String NumType
			{
				get
				{
					System.String data = entity.NumType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumType = null;
					else entity.NumType = Convert.ToString(value);
				}
			}
			public System.String NumDecimalDigits
			{
				get
				{
					System.Int32? data = entity.NumDecimalDigits;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumDecimalDigits = null;
					else entity.NumDecimalDigits = Convert.ToInt32(value);
				}
			}
			public System.String NumMinValue
			{
				get
				{
					System.Int32? data = entity.NumMinValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumMinValue = null;
					else entity.NumMinValue = Convert.ToInt32(value);
				}
			}
			public System.String NumMaxValue
			{
				get
				{
					System.Int32? data = entity.NumMaxValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumMaxValue = null;
					else entity.NumMaxValue = Convert.ToInt32(value);
				}
			}
			public System.String NumMaxLength
			{
				get
				{
					System.Int32? data = entity.NumMaxLength;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumMaxLength = null;
					else entity.NumMaxLength = Convert.ToInt32(value);
				}
			}
			public System.String IsMonitoring
			{
				get
				{
					System.Boolean? data = entity.IsMonitoring;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMonitoring = null;
					else entity.IsMonitoring = Convert.ToBoolean(value);
				}
			}
			public System.String IsChart
			{
				get
				{
					System.Boolean? data = entity.IsChart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChart = null;
					else entity.IsChart = Convert.ToBoolean(value);
				}
			}
			public System.String ChartColor
			{
				get
				{
					System.Int32? data = entity.ChartColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartColor = null;
					else entity.ChartColor = Convert.ToInt32(value);
				}
			}
			public System.String ChartMinValue
			{
				get
				{
					System.Int32? data = entity.ChartMinValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartMinValue = null;
					else entity.ChartMinValue = Convert.ToInt32(value);
				}
			}
			public System.String ChartMaxValue
			{
				get
				{
					System.Int32? data = entity.ChartMaxValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartMaxValue = null;
					else entity.ChartMaxValue = Convert.ToInt32(value);
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
			public System.String QuestionID
			{
				get
				{
					System.String data = entity.QuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionID = null;
					else entity.QuestionID = Convert.ToString(value);
				}
			}
			public System.String ParentVitalSignID
			{
				get
				{
					System.String data = entity.ParentVitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentVitalSignID = null;
					else entity.ParentVitalSignID = Convert.ToString(value);
				}
			}
			public System.String ChartYAxisStep
			{
				get
				{
					System.Decimal? data = entity.ChartYAxisStep;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartYAxisStep = null;
					else entity.ChartYAxisStep = Convert.ToDecimal(value);
				}
			}
			public System.String RowIndex
			{
				get
				{
					System.Int32? data = entity.RowIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RowIndex = null;
					else entity.RowIndex = Convert.ToInt32(value);
				}
			}
			private esVitalSign entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVitalSignQuery query)
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
				throw new Exception("esVitalSign can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VitalSign : esVitalSign
	{	
	}

	[Serializable]
	abstract public class esVitalSignQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return VitalSignMetadata.Meta();
			}
		}	
			
		public esQueryItem VitalSignID
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.VitalSignID, esSystemType.String);
			}
		} 
			
		public esQueryItem VitalSignName
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.VitalSignName, esSystemType.String);
			}
		} 
			
		public esQueryItem VitalSignInitial
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.VitalSignInitial, esSystemType.String);
			}
		} 
			
		public esQueryItem SRVitalSignGroup
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.SRVitalSignGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem RowIndexInGroup
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.RowIndexInGroup, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ValueType
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.ValueType, esSystemType.String);
			}
		} 
			
		public esQueryItem StandardReferenceID
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
			}
		} 
			
		public esQueryItem EntryMask
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.EntryMask, esSystemType.String);
			}
		} 
			
		public esQueryItem VitalSignUnit
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.VitalSignUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem NumType
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.NumType, esSystemType.String);
			}
		} 
			
		public esQueryItem NumDecimalDigits
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.NumDecimalDigits, esSystemType.Int32);
			}
		} 
			
		public esQueryItem NumMinValue
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.NumMinValue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem NumMaxValue
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.NumMaxValue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem NumMaxLength
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.NumMaxLength, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsMonitoring
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.IsMonitoring, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsChart
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.IsChart, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartColor
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.ChartColor, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartMinValue
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.ChartMinValue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartMaxValue
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.ChartMaxValue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParentVitalSignID
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.ParentVitalSignID, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartYAxisStep
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.ChartYAxisStep, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem RowIndex
		{
			get
			{
				return new esQueryItem(this, VitalSignMetadata.ColumnNames.RowIndex, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VitalSignCollection")]
	public partial class VitalSignCollection : esVitalSignCollection, IEnumerable< VitalSign>
	{
		public VitalSignCollection()
		{

		}	
		
		public static implicit operator List< VitalSign>(VitalSignCollection coll)
		{
			List< VitalSign> list = new List< VitalSign>();
			
			foreach (VitalSign emp in coll)
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
				return  VitalSignMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VitalSignQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VitalSign(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VitalSign();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public VitalSignQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VitalSignQuery();
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
		public bool Load(VitalSignQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VitalSign AddNew()
		{
			VitalSign entity = base.AddNewEntity() as VitalSign;
			
			return entity;		
		}
		public VitalSign FindByPrimaryKey(String vitalSignID)
		{
			return base.FindByPrimaryKey(vitalSignID) as VitalSign;
		}

		#region IEnumerable< VitalSign> Members

		IEnumerator< VitalSign> IEnumerable< VitalSign>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VitalSign;
			}
		}

		#endregion
		
		private VitalSignQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VitalSign' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VitalSign ({VitalSignID})")]
	[Serializable]
	public partial class VitalSign : esVitalSign
	{
		public VitalSign()
		{
		}	
	
		public VitalSign(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VitalSignMetadata.Meta();
			}
		}	
	
		override protected esVitalSignQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VitalSignQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public VitalSignQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VitalSignQuery();
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
		public bool Load(VitalSignQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private VitalSignQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VitalSignQuery : esVitalSignQuery
	{
		public VitalSignQuery()
		{

		}		
		
		public VitalSignQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "VitalSignQuery";
        }
	}

	[Serializable]
	public partial class VitalSignMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VitalSignMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.VitalSignID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.VitalSignID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.VitalSignName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.VitalSignName;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.VitalSignInitial, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.VitalSignInitial;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.SRVitalSignGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.SRVitalSignGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.RowIndexInGroup, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.RowIndexInGroup;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.ValueType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.ValueType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.StandardReferenceID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.StandardReferenceID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.EntryMask, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.EntryMask;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.VitalSignUnit, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.VitalSignUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.NumType, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.NumType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.NumDecimalDigits, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.NumDecimalDigits;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.NumMinValue, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.NumMinValue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.NumMaxValue, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.NumMaxValue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.NumMaxLength, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.NumMaxLength;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.IsMonitoring, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VitalSignMetadata.PropertyNames.IsMonitoring;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.IsChart, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VitalSignMetadata.PropertyNames.IsChart;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.ChartColor, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.ChartColor;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.ChartMinValue, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.ChartMinValue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.ChartMaxValue, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.ChartMaxValue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VitalSignMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.QuestionID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.QuestionID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.ParentVitalSignID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignMetadata.PropertyNames.ParentVitalSignID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.ChartYAxisStep, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VitalSignMetadata.PropertyNames.ChartYAxisStep;
			c.NumericPrecision = 4;
			c.NumericScale = 1;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignMetadata.ColumnNames.RowIndex, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignMetadata.PropertyNames.RowIndex;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public VitalSignMetadata Meta()
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
			public const string VitalSignID = "VitalSignID";
			public const string VitalSignName = "VitalSignName";
			public const string VitalSignInitial = "VitalSignInitial";
			public const string SRVitalSignGroup = "SRVitalSignGroup";
			public const string RowIndexInGroup = "RowIndexInGroup";
			public const string ValueType = "ValueType";
			public const string StandardReferenceID = "StandardReferenceID";
			public const string EntryMask = "EntryMask";
			public const string VitalSignUnit = "VitalSignUnit";
			public const string NumType = "NumType";
			public const string NumDecimalDigits = "NumDecimalDigits";
			public const string NumMinValue = "NumMinValue";
			public const string NumMaxValue = "NumMaxValue";
			public const string NumMaxLength = "NumMaxLength";
			public const string IsMonitoring = "IsMonitoring";
			public const string IsChart = "IsChart";
			public const string ChartColor = "ChartColor";
			public const string ChartMinValue = "ChartMinValue";
			public const string ChartMaxValue = "ChartMaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string QuestionID = "QuestionID";
			public const string ParentVitalSignID = "ParentVitalSignID";
			public const string ChartYAxisStep = "ChartYAxisStep";
			public const string RowIndex = "RowIndex";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string VitalSignID = "VitalSignID";
			public const string VitalSignName = "VitalSignName";
			public const string VitalSignInitial = "VitalSignInitial";
			public const string SRVitalSignGroup = "SRVitalSignGroup";
			public const string RowIndexInGroup = "RowIndexInGroup";
			public const string ValueType = "ValueType";
			public const string StandardReferenceID = "StandardReferenceID";
			public const string EntryMask = "EntryMask";
			public const string VitalSignUnit = "VitalSignUnit";
			public const string NumType = "NumType";
			public const string NumDecimalDigits = "NumDecimalDigits";
			public const string NumMinValue = "NumMinValue";
			public const string NumMaxValue = "NumMaxValue";
			public const string NumMaxLength = "NumMaxLength";
			public const string IsMonitoring = "IsMonitoring";
			public const string IsChart = "IsChart";
			public const string ChartColor = "ChartColor";
			public const string ChartMinValue = "ChartMinValue";
			public const string ChartMaxValue = "ChartMaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string QuestionID = "QuestionID";
			public const string ParentVitalSignID = "ParentVitalSignID";
			public const string ChartYAxisStep = "ChartYAxisStep";
			public const string RowIndex = "RowIndex";
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
			lock (typeof(VitalSignMetadata))
			{
				if(VitalSignMetadata.mapDelegates == null)
				{
					VitalSignMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VitalSignMetadata.meta == null)
				{
					VitalSignMetadata.meta = new VitalSignMetadata();
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
				
				meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignInitial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRVitalSignGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RowIndexInGroup", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValueType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StandardReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EntryMask", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumDecimalDigits", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NumMinValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NumMaxValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NumMaxLength", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsMonitoring", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsChart", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartColor", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartMinValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartMaxValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentVitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartYAxisStep", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RowIndex", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "VitalSign";
				meta.Destination = "VitalSign";
				meta.spInsert = "proc_VitalSignInsert";				
				meta.spUpdate = "proc_VitalSignUpdate";		
				meta.spDelete = "proc_VitalSignDelete";
				meta.spLoadAll = "proc_VitalSignLoadAll";
				meta.spLoadByPrimaryKey = "proc_VitalSignLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VitalSignMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
