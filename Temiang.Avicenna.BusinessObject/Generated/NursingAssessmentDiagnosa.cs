/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/15/2021 2:53:06 PM
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
	abstract public class esNursingAssessmentDiagnosaCollection : esEntityCollectionWAuditLog
	{
		public esNursingAssessmentDiagnosaCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NursingAssessmentDiagnosaCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNursingAssessmentDiagnosaQuery query)
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
			this.InitQuery(query as esNursingAssessmentDiagnosaQuery);
		}
		#endregion
			
		virtual public NursingAssessmentDiagnosa DetachEntity(NursingAssessmentDiagnosa entity)
		{
			return base.DetachEntity(entity) as NursingAssessmentDiagnosa;
		}
		
		virtual public NursingAssessmentDiagnosa AttachEntity(NursingAssessmentDiagnosa entity)
		{
			return base.AttachEntity(entity) as NursingAssessmentDiagnosa;
		}
		
		virtual public void Combine(NursingAssessmentDiagnosaCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingAssessmentDiagnosa this[int index]
		{
			get
			{
				return base[index] as NursingAssessmentDiagnosa;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingAssessmentDiagnosa);
		}
	}

	[Serializable]
	abstract public class esNursingAssessmentDiagnosa : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingAssessmentDiagnosaQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNursingAssessmentDiagnosa()
		{
		}
	
		public esNursingAssessmentDiagnosa(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 iD)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 iD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 iD)
		{
			esNursingAssessmentDiagnosaQuery query = this.GetDynamicQuery();
			query.Where(query.ID==iD);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 iD)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",iD);
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
						case "QuestionID": this.str.QuestionID = (string)value; break;
						case "NursingDiagnosaID": this.str.NursingDiagnosaID = (string)value; break;
						case "SRAnswerType": this.str.SRAnswerType = (string)value; break;
						case "Operand": this.str.Operand = (string)value; break;
						case "AcceptedText": this.str.AcceptedText = (string)value; break;
						case "AcceptedNum": this.str.AcceptedNum = (string)value; break;
						case "CheckValue": this.str.CheckValue = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsUsingRange": this.str.IsUsingRange = (string)value; break;
						case "AcceptedNum2": this.str.AcceptedNum2 = (string)value; break;
						case "AgeInMonthStart": this.str.AgeInMonthStart = (string)value; break;
						case "AgeInMonthEnd": this.str.AgeInMonthEnd = (string)value; break;
						case "SRNsDiagnosaPrefix": this.str.SRNsDiagnosaPrefix = (string)value; break;
						case "SRNsDiagnosaSuffix": this.str.SRNsDiagnosaSuffix = (string)value; break;
						case "ShowAssessmetAsPrefix": this.str.ShowAssessmetAsPrefix = (string)value; break;
						case "ShowAssessmetAsSuffix": this.str.ShowAssessmetAsSuffix = (string)value; break;
						case "ID": this.str.ID = (string)value; break;
						case "SRNsMandatoryLevel": this.str.SRNsMandatoryLevel = (string)value; break;
						case "Sex": this.str.Sex = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AcceptedNum":
						
							if (value == null || value is System.Decimal)
								this.AcceptedNum = (System.Decimal?)value;
							break;
						case "CheckValue":
						
							if (value == null || value is System.Boolean)
								this.CheckValue = (System.Boolean?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsUsingRange":
						
							if (value == null || value is System.Boolean)
								this.IsUsingRange = (System.Boolean?)value;
							break;
						case "AcceptedNum2":
						
							if (value == null || value is System.Decimal)
								this.AcceptedNum2 = (System.Decimal?)value;
							break;
						case "AgeInMonthStart":
						
							if (value == null || value is System.Int32)
								this.AgeInMonthStart = (System.Int32?)value;
							break;
						case "AgeInMonthEnd":
						
							if (value == null || value is System.Int32)
								this.AgeInMonthEnd = (System.Int32?)value;
							break;
						case "ShowAssessmetAsPrefix":
						
							if (value == null || value is System.Boolean)
								this.ShowAssessmetAsPrefix = (System.Boolean?)value;
							break;
						case "ShowAssessmetAsSuffix":
						
							if (value == null || value is System.Boolean)
								this.ShowAssessmetAsSuffix = (System.Boolean?)value;
							break;
						case "ID":
						
							if (value == null || value is System.Int32)
								this.ID = (System.Int32?)value;
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
		/// Maps to NursingAssessmentDiagnosa.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.QuestionID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.NursingDiagnosaID
		/// </summary>
		virtual public System.String NursingDiagnosaID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.NursingDiagnosaID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.NursingDiagnosaID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.SRAnswerType
		/// </summary>
		virtual public System.String SRAnswerType
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRAnswerType);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRAnswerType, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.Operand
		/// </summary>
		virtual public System.String Operand
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.Operand);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.Operand, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.AcceptedText
		/// </summary>
		virtual public System.String AcceptedText
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedText);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedText, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.AcceptedNum
		/// </summary>
		virtual public System.Decimal? AcceptedNum
		{
			get
			{
				return base.GetSystemDecimal(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum);
			}
			
			set
			{
				base.SetSystemDecimal(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.CheckValue
		/// </summary>
		virtual public System.Boolean? CheckValue
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.CheckValue);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.CheckValue, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentDiagnosaMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentDiagnosaMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.IsUsingRange
		/// </summary>
		virtual public System.Boolean? IsUsingRange
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.IsUsingRange);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.IsUsingRange, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.AcceptedNum2
		/// </summary>
		virtual public System.Decimal? AcceptedNum2
		{
			get
			{
				return base.GetSystemDecimal(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum2);
			}
			
			set
			{
				base.SetSystemDecimal(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum2, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.AgeInMonthStart
		/// </summary>
		virtual public System.Int32? AgeInMonthStart
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.AgeInMonthEnd
		/// </summary>
		virtual public System.Int32? AgeInMonthEnd
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.SRNsDiagnosaPrefix
		/// </summary>
		virtual public System.String SRNsDiagnosaPrefix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaPrefix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaPrefix, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.SRNsDiagnosaSuffix
		/// </summary>
		virtual public System.String SRNsDiagnosaSuffix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaSuffix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaSuffix, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.ShowAssessmetAsPrefix
		/// </summary>
		virtual public System.Boolean? ShowAssessmetAsPrefix
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsPrefix);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsPrefix, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.ShowAssessmetAsSuffix
		/// </summary>
		virtual public System.Boolean? ShowAssessmetAsSuffix
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsSuffix);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsSuffix, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.ID
		/// </summary>
		virtual public System.Int32? ID
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentDiagnosaMetadata.ColumnNames.ID);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentDiagnosaMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.SRNsMandatoryLevel
		/// </summary>
		virtual public System.String SRNsMandatoryLevel
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsMandatoryLevel);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsMandatoryLevel, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentDiagnosa.Sex
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.Sex);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentDiagnosaMetadata.ColumnNames.Sex, value);
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
			public esStrings(esNursingAssessmentDiagnosa entity)
			{
				this.entity = entity;
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
			public System.String NursingDiagnosaID
			{
				get
				{
					System.String data = entity.NursingDiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaID = null;
					else entity.NursingDiagnosaID = Convert.ToString(value);
				}
			}
			public System.String SRAnswerType
			{
				get
				{
					System.String data = entity.SRAnswerType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAnswerType = null;
					else entity.SRAnswerType = Convert.ToString(value);
				}
			}
			public System.String Operand
			{
				get
				{
					System.String data = entity.Operand;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Operand = null;
					else entity.Operand = Convert.ToString(value);
				}
			}
			public System.String AcceptedText
			{
				get
				{
					System.String data = entity.AcceptedText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcceptedText = null;
					else entity.AcceptedText = Convert.ToString(value);
				}
			}
			public System.String AcceptedNum
			{
				get
				{
					System.Decimal? data = entity.AcceptedNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcceptedNum = null;
					else entity.AcceptedNum = Convert.ToDecimal(value);
				}
			}
			public System.String CheckValue
			{
				get
				{
					System.Boolean? data = entity.CheckValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckValue = null;
					else entity.CheckValue = Convert.ToBoolean(value);
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
			public System.String IsUsingRange
			{
				get
				{
					System.Boolean? data = entity.IsUsingRange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingRange = null;
					else entity.IsUsingRange = Convert.ToBoolean(value);
				}
			}
			public System.String AcceptedNum2
			{
				get
				{
					System.Decimal? data = entity.AcceptedNum2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcceptedNum2 = null;
					else entity.AcceptedNum2 = Convert.ToDecimal(value);
				}
			}
			public System.String AgeInMonthStart
			{
				get
				{
					System.Int32? data = entity.AgeInMonthStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeInMonthStart = null;
					else entity.AgeInMonthStart = Convert.ToInt32(value);
				}
			}
			public System.String AgeInMonthEnd
			{
				get
				{
					System.Int32? data = entity.AgeInMonthEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeInMonthEnd = null;
					else entity.AgeInMonthEnd = Convert.ToInt32(value);
				}
			}
			public System.String SRNsDiagnosaPrefix
			{
				get
				{
					System.String data = entity.SRNsDiagnosaPrefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsDiagnosaPrefix = null;
					else entity.SRNsDiagnosaPrefix = Convert.ToString(value);
				}
			}
			public System.String SRNsDiagnosaSuffix
			{
				get
				{
					System.String data = entity.SRNsDiagnosaSuffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsDiagnosaSuffix = null;
					else entity.SRNsDiagnosaSuffix = Convert.ToString(value);
				}
			}
			public System.String ShowAssessmetAsPrefix
			{
				get
				{
					System.Boolean? data = entity.ShowAssessmetAsPrefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShowAssessmetAsPrefix = null;
					else entity.ShowAssessmetAsPrefix = Convert.ToBoolean(value);
				}
			}
			public System.String ShowAssessmetAsSuffix
			{
				get
				{
					System.Boolean? data = entity.ShowAssessmetAsSuffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShowAssessmetAsSuffix = null;
					else entity.ShowAssessmetAsSuffix = Convert.ToBoolean(value);
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
			public System.String SRNsMandatoryLevel
			{
				get
				{
					System.String data = entity.SRNsMandatoryLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsMandatoryLevel = null;
					else entity.SRNsMandatoryLevel = Convert.ToString(value);
				}
			}
			public System.String Sex
			{
				get
				{
					System.String data = entity.Sex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sex = null;
					else entity.Sex = Convert.ToString(value);
				}
			}
			private esNursingAssessmentDiagnosa entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingAssessmentDiagnosaQuery query)
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
				throw new Exception("esNursingAssessmentDiagnosa can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}
    

    [Serializable]
	abstract public class esNursingAssessmentDiagnosaQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentDiagnosaMetadata.Meta();
			}
		}	
			
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
			
		public esQueryItem NursingDiagnosaID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.NursingDiagnosaID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRAnswerType
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.SRAnswerType, esSystemType.String);
			}
		} 
			
		public esQueryItem Operand
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.Operand, esSystemType.String);
			}
		} 
			
		public esQueryItem AcceptedText
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedText, esSystemType.String);
			}
		} 
			
		public esQueryItem AcceptedNum
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CheckValue
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.CheckValue, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsUsingRange
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.IsUsingRange, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem AcceptedNum2
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum2, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AgeInMonthStart
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AgeInMonthEnd
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRNsDiagnosaPrefix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaPrefix, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNsDiagnosaSuffix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaSuffix, esSystemType.String);
			}
		} 
			
		public esQueryItem ShowAssessmetAsPrefix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsPrefix, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ShowAssessmetAsSuffix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsSuffix, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.ID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRNsMandatoryLevel
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsMandatoryLevel, esSystemType.String);
			}
		} 
			
		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentDiagnosaMetadata.ColumnNames.Sex, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingAssessmentDiagnosaCollection")]
	public partial class NursingAssessmentDiagnosaCollection : esNursingAssessmentDiagnosaCollection, IEnumerable< NursingAssessmentDiagnosa>
	{
		public NursingAssessmentDiagnosaCollection()
		{

		}	
		
		public static implicit operator List< NursingAssessmentDiagnosa>(NursingAssessmentDiagnosaCollection coll)
		{
			List< NursingAssessmentDiagnosa> list = new List< NursingAssessmentDiagnosa>();
			
			foreach (NursingAssessmentDiagnosa emp in coll)
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
				return  NursingAssessmentDiagnosaMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentDiagnosaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingAssessmentDiagnosa(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingAssessmentDiagnosa();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NursingAssessmentDiagnosaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentDiagnosaQuery();
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
		public bool Load(NursingAssessmentDiagnosaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NursingAssessmentDiagnosa AddNew()
		{
			NursingAssessmentDiagnosa entity = base.AddNewEntity() as NursingAssessmentDiagnosa;
			
			return entity;		
		}
		public NursingAssessmentDiagnosa FindByPrimaryKey(Int32 iD)
		{
			return base.FindByPrimaryKey(iD) as NursingAssessmentDiagnosa;
		}

		#region IEnumerable< NursingAssessmentDiagnosa> Members

		IEnumerator< NursingAssessmentDiagnosa> IEnumerable< NursingAssessmentDiagnosa>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingAssessmentDiagnosa;
			}
		}

		#endregion
		
		private NursingAssessmentDiagnosaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingAssessmentDiagnosa' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NursingAssessmentDiagnosa ({ID})")]
	[Serializable]
	public partial class NursingAssessmentDiagnosa : esNursingAssessmentDiagnosa
	{
		public NursingAssessmentDiagnosa()
		{
		}	
	
		public NursingAssessmentDiagnosa(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentDiagnosaMetadata.Meta();
			}
		}	
	
		override protected esNursingAssessmentDiagnosaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentDiagnosaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NursingAssessmentDiagnosaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentDiagnosaQuery();
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
		public bool Load(NursingAssessmentDiagnosaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NursingAssessmentDiagnosaQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NursingAssessmentDiagnosaQuery : esNursingAssessmentDiagnosaQuery
	{
		public NursingAssessmentDiagnosaQuery()
		{

		}		
		
		public NursingAssessmentDiagnosaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NursingAssessmentDiagnosaQuery";
        }
	}

	[Serializable]
	public partial class NursingAssessmentDiagnosaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingAssessmentDiagnosaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.QuestionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.QuestionID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.NursingDiagnosaID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.NursingDiagnosaID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.SRAnswerType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.SRAnswerType;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.Operand, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.Operand;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedText, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.AcceptedText;
			c.CharacterMaxLength = 350;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.AcceptedNum;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.CheckValue, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.CheckValue;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.IsUsingRange, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.IsUsingRange;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum2, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.AcceptedNum2;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.AgeInMonthStart;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.AgeInMonthEnd;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaPrefix, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.SRNsDiagnosaPrefix;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaSuffix, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.SRNsDiagnosaSuffix;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsPrefix, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.ShowAssessmetAsPrefix;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsSuffix, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.ShowAssessmetAsSuffix;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.ID, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsMandatoryLevel, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.SRNsMandatoryLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentDiagnosaMetadata.ColumnNames.Sex, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentDiagnosaMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NursingAssessmentDiagnosaMetadata Meta()
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
			public const string QuestionID = "QuestionID";
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string SRAnswerType = "SRAnswerType";
			public const string Operand = "Operand";
			public const string AcceptedText = "AcceptedText";
			public const string AcceptedNum = "AcceptedNum";
			public const string CheckValue = "CheckValue";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsUsingRange = "IsUsingRange";
			public const string AcceptedNum2 = "AcceptedNum2";
			public const string AgeInMonthStart = "AgeInMonthStart";
			public const string AgeInMonthEnd = "AgeInMonthEnd";
			public const string SRNsDiagnosaPrefix = "SRNsDiagnosaPrefix";
			public const string SRNsDiagnosaSuffix = "SRNsDiagnosaSuffix";
			public const string ShowAssessmetAsPrefix = "ShowAssessmetAsPrefix";
			public const string ShowAssessmetAsSuffix = "ShowAssessmetAsSuffix";
			public const string ID = "ID";
			public const string SRNsMandatoryLevel = "SRNsMandatoryLevel";
			public const string Sex = "Sex";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string QuestionID = "QuestionID";
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string SRAnswerType = "SRAnswerType";
			public const string Operand = "Operand";
			public const string AcceptedText = "AcceptedText";
			public const string AcceptedNum = "AcceptedNum";
			public const string CheckValue = "CheckValue";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsUsingRange = "IsUsingRange";
			public const string AcceptedNum2 = "AcceptedNum2";
			public const string AgeInMonthStart = "AgeInMonthStart";
			public const string AgeInMonthEnd = "AgeInMonthEnd";
			public const string SRNsDiagnosaPrefix = "SRNsDiagnosaPrefix";
			public const string SRNsDiagnosaSuffix = "SRNsDiagnosaSuffix";
			public const string ShowAssessmetAsPrefix = "ShowAssessmetAsPrefix";
			public const string ShowAssessmetAsSuffix = "ShowAssessmetAsSuffix";
			public const string ID = "ID";
			public const string SRNsMandatoryLevel = "SRNsMandatoryLevel";
			public const string Sex = "Sex";
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
			lock (typeof(NursingAssessmentDiagnosaMetadata))
			{
				if(NursingAssessmentDiagnosaMetadata.mapDelegates == null)
				{
					NursingAssessmentDiagnosaMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingAssessmentDiagnosaMetadata.meta == null)
				{
					NursingAssessmentDiagnosaMetadata.meta = new NursingAssessmentDiagnosaMetadata();
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
				
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingDiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAnswerType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Operand", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AcceptedText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AcceptedNum", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CheckValue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsUsingRange", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AcceptedNum2", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AgeInMonthStart", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AgeInMonthEnd", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRNsDiagnosaPrefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNsDiagnosaSuffix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShowAssessmetAsPrefix", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ShowAssessmetAsSuffix", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRNsMandatoryLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "NursingAssessmentDiagnosa";
				meta.Destination = "NursingAssessmentDiagnosa";
				meta.spInsert = "proc_NursingAssessmentDiagnosaInsert";				
				meta.spUpdate = "proc_NursingAssessmentDiagnosaUpdate";		
				meta.spDelete = "proc_NursingAssessmentDiagnosaDelete";
				meta.spLoadAll = "proc_NursingAssessmentDiagnosaLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingAssessmentDiagnosaLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingAssessmentDiagnosaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
