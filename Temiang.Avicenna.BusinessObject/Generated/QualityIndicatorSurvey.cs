/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/10/2022 2:25:55 PM
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
	abstract public class esQualityIndicatorSurveyCollection : esEntityCollectionWAuditLog
	{
		public esQualityIndicatorSurveyCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "QualityIndicatorSurveyCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esQualityIndicatorSurveyQuery query)
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
			this.InitQuery(query as esQualityIndicatorSurveyQuery);
		}
		#endregion
			
		virtual public QualityIndicatorSurvey DetachEntity(QualityIndicatorSurvey entity)
		{
			return base.DetachEntity(entity) as QualityIndicatorSurvey;
		}
		
		virtual public QualityIndicatorSurvey AttachEntity(QualityIndicatorSurvey entity)
		{
			return base.AttachEntity(entity) as QualityIndicatorSurvey;
		}
		
		virtual public void Combine(QualityIndicatorSurveyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QualityIndicatorSurvey this[int index]
		{
			get
			{
				return base[index] as QualityIndicatorSurvey;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QualityIndicatorSurvey);
		}
	}

	[Serializable]
	abstract public class esQualityIndicatorSurvey : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQualityIndicatorSurveyQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esQualityIndicatorSurvey()
		{
		}
	
		public esQualityIndicatorSurvey(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 surveyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(surveyID);
			else
				return LoadByPrimaryKeyStoredProcedure(surveyID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 surveyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(surveyID);
			else
				return LoadByPrimaryKeyStoredProcedure(surveyID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 surveyID)
		{
			esQualityIndicatorSurveyQuery query = this.GetDynamicQuery();
			query.Where(query.SurveyID==surveyID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 surveyID)
		{
			esParameters parms = new esParameters();
			parms.Add("SurveyID",surveyID);
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
						case "SurveyID": this.str.SurveyID = (string)value; break;
						case "StandardReferenceID": this.str.StandardReferenceID = (string)value; break;
						case "PeriodDate": this.str.PeriodDate = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsApprove": this.str.IsApprove = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidNotes": this.str.VoidNotes = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SurveyID":
						
							if (value == null || value is System.Int32)
								this.SurveyID = (System.Int32?)value;
							break;
						case "PeriodDate":
						
							if (value == null || value is System.DateTime)
								this.PeriodDate = (System.DateTime?)value;
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
		/// Maps to QualityIndicatorSurvey.SurveyID
		/// </summary>
		virtual public System.Int32? SurveyID
		{
			get
			{
				return base.GetSystemInt32(QualityIndicatorSurveyMetadata.ColumnNames.SurveyID);
			}
			
			set
			{
				base.SetSystemInt32(QualityIndicatorSurveyMetadata.ColumnNames.SurveyID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.StandardReferenceID
		/// </summary>
		virtual public System.String StandardReferenceID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.StandardReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.PeriodDate
		/// </summary>
		virtual public System.DateTime? PeriodDate
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.PeriodDate);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.PeriodDate, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(QualityIndicatorSurveyMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(QualityIndicatorSurveyMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(QualityIndicatorSurveyMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(QualityIndicatorSurveyMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.VoidNotes
		/// </summary>
		virtual public System.String VoidNotes
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.VoidNotes);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.VoidNotes, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurvey.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyMetadata.ColumnNames.ServiceUnitID, value);
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
			public esStrings(esQualityIndicatorSurvey entity)
			{
				this.entity = entity;
			}
			public System.String SurveyID
			{
				get
				{
					System.Int32? data = entity.SurveyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SurveyID = null;
					else entity.SurveyID = Convert.ToInt32(value);
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
			public System.String PeriodDate
			{
				get
				{
					System.DateTime? data = entity.PeriodDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodDate = null;
					else entity.PeriodDate = Convert.ToDateTime(value);
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
			private esQualityIndicatorSurvey entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQualityIndicatorSurveyQuery query)
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
				throw new Exception("esQualityIndicatorSurvey can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class QualityIndicatorSurvey : esQualityIndicatorSurvey
	{	
	}

	[Serializable]
	abstract public class esQualityIndicatorSurveyQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return QualityIndicatorSurveyMetadata.Meta();
			}
		}	
			
		public esQueryItem SurveyID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.SurveyID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem StandardReferenceID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
			}
		} 
			
		public esQueryItem PeriodDate
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.PeriodDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidNotes
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.VoidNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QualityIndicatorSurveyCollection")]
	public partial class QualityIndicatorSurveyCollection : esQualityIndicatorSurveyCollection, IEnumerable< QualityIndicatorSurvey>
	{
		public QualityIndicatorSurveyCollection()
		{

		}	
		
		public static implicit operator List< QualityIndicatorSurvey>(QualityIndicatorSurveyCollection coll)
		{
			List< QualityIndicatorSurvey> list = new List< QualityIndicatorSurvey>();
			
			foreach (QualityIndicatorSurvey emp in coll)
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
				return  QualityIndicatorSurveyMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QualityIndicatorSurveyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QualityIndicatorSurvey(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QualityIndicatorSurvey();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public QualityIndicatorSurveyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QualityIndicatorSurveyQuery();
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
		public bool Load(QualityIndicatorSurveyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public QualityIndicatorSurvey AddNew()
		{
			QualityIndicatorSurvey entity = base.AddNewEntity() as QualityIndicatorSurvey;
			
			return entity;		
		}
		public QualityIndicatorSurvey FindByPrimaryKey(Int32 surveyID)
		{
			return base.FindByPrimaryKey(surveyID) as QualityIndicatorSurvey;
		}

		#region IEnumerable< QualityIndicatorSurvey> Members

		IEnumerator< QualityIndicatorSurvey> IEnumerable< QualityIndicatorSurvey>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QualityIndicatorSurvey;
			}
		}

		#endregion
		
		private QualityIndicatorSurveyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QualityIndicatorSurvey' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("QualityIndicatorSurvey ({SurveyID})")]
	[Serializable]
	public partial class QualityIndicatorSurvey : esQualityIndicatorSurvey
	{
		public QualityIndicatorSurvey()
		{
		}	
	
		public QualityIndicatorSurvey(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QualityIndicatorSurveyMetadata.Meta();
			}
		}	
	
		override protected esQualityIndicatorSurveyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QualityIndicatorSurveyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public QualityIndicatorSurveyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QualityIndicatorSurveyQuery();
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
		public bool Load(QualityIndicatorSurveyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private QualityIndicatorSurveyQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class QualityIndicatorSurveyQuery : esQualityIndicatorSurveyQuery
	{
		public QualityIndicatorSurveyQuery()
		{

		}		
		
		public QualityIndicatorSurveyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "QualityIndicatorSurveyQuery";
        }
	}

	[Serializable]
	public partial class QualityIndicatorSurveyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QualityIndicatorSurveyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.SurveyID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.SurveyID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.StandardReferenceID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.StandardReferenceID;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.PeriodDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.PeriodDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.CreateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.IsApprove, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.IsApprove;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.ApprovedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.IsVoid, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.VoidDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.VoidNotes, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.VoidNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyMetadata.ColumnNames.ServiceUnitID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public QualityIndicatorSurveyMetadata Meta()
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
			public const string SurveyID = "SurveyID";
			public const string StandardReferenceID = "StandardReferenceID";
			public const string PeriodDate = "PeriodDate";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApprove = "IsApprove";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidNotes = "VoidNotes";
			public const string ServiceUnitID = "ServiceUnitID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SurveyID = "SurveyID";
			public const string StandardReferenceID = "StandardReferenceID";
			public const string PeriodDate = "PeriodDate";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApprove = "IsApprove";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidNotes = "VoidNotes";
			public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(QualityIndicatorSurveyMetadata))
			{
				if(QualityIndicatorSurveyMetadata.mapDelegates == null)
				{
					QualityIndicatorSurveyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QualityIndicatorSurveyMetadata.meta == null)
				{
					QualityIndicatorSurveyMetadata.meta = new QualityIndicatorSurveyMetadata();
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
				
				meta.AddTypeMap("SurveyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StandardReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "QualityIndicatorSurvey";
				meta.Destination = "QualityIndicatorSurvey";
				meta.spInsert = "proc_QualityIndicatorSurveyInsert";				
				meta.spUpdate = "proc_QualityIndicatorSurveyUpdate";		
				meta.spDelete = "proc_QualityIndicatorSurveyDelete";
				meta.spLoadAll = "proc_QualityIndicatorSurveyLoadAll";
				meta.spLoadByPrimaryKey = "proc_QualityIndicatorSurveyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QualityIndicatorSurveyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
