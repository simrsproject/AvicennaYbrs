/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/4/2023 8:41:31 PM
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
	abstract public class esDocumentFilesCollection : esEntityCollectionWAuditLog
	{
		public esDocumentFilesCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "DocumentFilesCollection";
		}

		#region Query Logic
		protected void InitQuery(esDocumentFilesQuery query)
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
			this.InitQuery(query as esDocumentFilesQuery);
		}
		#endregion

		virtual public DocumentFiles DetachEntity(DocumentFiles entity)
		{
			return base.DetachEntity(entity) as DocumentFiles;
		}

		virtual public DocumentFiles AttachEntity(DocumentFiles entity)
		{
			return base.AttachEntity(entity) as DocumentFiles;
		}

		virtual public void Combine(DocumentFilesCollection collection)
		{
			base.Combine(collection);
		}

		new public DocumentFiles this[int index]
		{
			get
			{
				return base[index] as DocumentFiles;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DocumentFiles);
		}
	}

	[Serializable]
	abstract public class esDocumentFiles : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDocumentFilesQuery GetDynamicQuery()
		{
			return null;
		}

		public esDocumentFiles()
		{
		}

		public esDocumentFiles(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 documentFilesID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentFilesID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 documentFilesID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentFilesID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 documentFilesID)
		{
			esDocumentFilesQuery query = this.GetDynamicQuery();
			query.Where(query.DocumentFilesID == documentFilesID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 documentFilesID)
		{
			esParameters parms = new esParameters();
			parms.Add("DocumentFilesID", documentFilesID);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "DocumentFilesID": this.str.DocumentFilesID = (string)value; break;
						case "DocumentName": this.str.DocumentName = (string)value; break;
						case "DocumentNumber": this.str.DocumentNumber = (string)value; break;
						case "FileTemplateName": this.str.FileTemplateName = (string)value; break;
						case "IsQuality": this.str.IsQuality = (string)value; break;
						case "IsLegible": this.str.IsLegible = (string)value; break;
						case "IsSign": this.str.IsSign = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsUsedForAnalysis": this.str.IsUsedForAnalysis = (string)value; break;
						case "IsUsedForGuarantorChecklist": this.str.IsUsedForGuarantorChecklist = (string)value; break;
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "DocumentInitial": this.str.DocumentInitial = (string)value; break;
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "SRDocumentFileType": this.str.SRDocumentFileType = (string)value; break;
						case "SRAssessmentType": this.str.SRAssessmentType = (string)value; break;
						case "SRHaisMonitoring": this.str.SRHaisMonitoring = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DocumentFilesID":

							if (value == null || value is System.Int32)
								this.DocumentFilesID = (System.Int32?)value;
							break;
						case "IsQuality":

							if (value == null || value is System.Boolean)
								this.IsQuality = (System.Boolean?)value;
							break;
						case "IsLegible":

							if (value == null || value is System.Boolean)
								this.IsLegible = (System.Boolean?)value;
							break;
						case "IsSign":

							if (value == null || value is System.Boolean)
								this.IsSign = (System.Boolean?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsUsedForAnalysis":

							if (value == null || value is System.Boolean)
								this.IsUsedForAnalysis = (System.Boolean?)value;
							break;
						case "IsUsedForGuarantorChecklist":

							if (value == null || value is System.Boolean)
								this.IsUsedForGuarantorChecklist = (System.Boolean?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to DocumentFiles.DocumentFilesID
		/// </summary>
		virtual public System.Int32? DocumentFilesID
		{
			get
			{
				return base.GetSystemInt32(DocumentFilesMetadata.ColumnNames.DocumentFilesID);
			}

			set
			{
				base.SetSystemInt32(DocumentFilesMetadata.ColumnNames.DocumentFilesID, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.DocumentNumber
		/// </summary>
		virtual public System.String DocumentNumber
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.DocumentNumber);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.DocumentNumber, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.FileTemplateName
		/// </summary>
		virtual public System.String FileTemplateName
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.FileTemplateName);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.FileTemplateName, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.IsQuality
		/// </summary>
		virtual public System.Boolean? IsQuality
		{
			get
			{
				return base.GetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsQuality);
			}

			set
			{
				base.SetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsQuality, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.IsLegible
		/// </summary>
		virtual public System.Boolean? IsLegible
		{
			get
			{
				return base.GetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsLegible);
			}

			set
			{
				base.SetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsLegible, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.IsSign
		/// </summary>
		virtual public System.Boolean? IsSign
		{
			get
			{
				return base.GetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsSign);
			}

			set
			{
				base.SetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsSign, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DocumentFilesMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(DocumentFilesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.IsUsedForAnalysis
		/// </summary>
		virtual public System.Boolean? IsUsedForAnalysis
		{
			get
			{
				return base.GetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsUsedForAnalysis);
			}

			set
			{
				base.SetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsUsedForAnalysis, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.IsUsedForGuarantorChecklist
		/// </summary>
		virtual public System.Boolean? IsUsedForGuarantorChecklist
		{
			get
			{
				return base.GetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsUsedForGuarantorChecklist);
			}

			set
			{
				base.SetSystemBoolean(DocumentFilesMetadata.ColumnNames.IsUsedForGuarantorChecklist, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.ProgramID);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.DocumentInitial
		/// </summary>
		virtual public System.String DocumentInitial
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.DocumentInitial);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.DocumentInitial, value);
			}
		}
		/// <summary>
		/// Maps to DocumentFiles.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(DocumentFilesMetadata.ColumnNames.QuestionFormID);
			}

			set
			{
				base.SetSystemString(DocumentFilesMetadata.ColumnNames.QuestionFormID, value);
			}
		}
        /// <summary>
        /// Maps to DocumentFiles.SRDocumentFileType
        /// </summary>
        virtual public System.String SRDocumentFileType
        {
            get
            {
                return base.GetSystemString(DocumentFilesMetadata.ColumnNames.SRDocumentFileType);
            }

            set
            {
                base.SetSystemString(DocumentFilesMetadata.ColumnNames.SRDocumentFileType, value);
            }
        }
        /// <summary>
        /// Maps to DocumentFiles.SRAssessmentType
        /// </summary>
        virtual public System.String SRAssessmentType
        {
            get
            {
                return base.GetSystemString(DocumentFilesMetadata.ColumnNames.SRAssessmentType);
            }

            set
            {
                base.SetSystemString(DocumentFilesMetadata.ColumnNames.SRAssessmentType, value);
            }
        }
        /// <summary>
        /// Maps to DocumentFiles.SRHaisMonitoring
        /// </summary>
        virtual public System.String SRHaisMonitoring
        {
            get
            {
                return base.GetSystemString(DocumentFilesMetadata.ColumnNames.SRHaisMonitoring);
            }

            set
            {
                base.SetSystemString(DocumentFilesMetadata.ColumnNames.SRHaisMonitoring, value);
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
        [BrowsableAttribute(false)]
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
			public esStrings(esDocumentFiles entity)
			{
				this.entity = entity;
			}
			public System.String DocumentFilesID
			{
				get
				{
					System.Int32? data = entity.DocumentFilesID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentFilesID = null;
					else entity.DocumentFilesID = Convert.ToInt32(value);
				}
			}
			public System.String DocumentName
			{
				get
				{
					System.String data = entity.DocumentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentName = null;
					else entity.DocumentName = Convert.ToString(value);
				}
			}
			public System.String DocumentNumber
			{
				get
				{
					System.String data = entity.DocumentNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentNumber = null;
					else entity.DocumentNumber = Convert.ToString(value);
				}
			}
			public System.String FileTemplateName
			{
				get
				{
					System.String data = entity.FileTemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FileTemplateName = null;
					else entity.FileTemplateName = Convert.ToString(value);
				}
			}
			public System.String IsQuality
			{
				get
				{
					System.Boolean? data = entity.IsQuality;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsQuality = null;
					else entity.IsQuality = Convert.ToBoolean(value);
				}
			}
			public System.String IsLegible
			{
				get
				{
					System.Boolean? data = entity.IsLegible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLegible = null;
					else entity.IsLegible = Convert.ToBoolean(value);
				}
			}
			public System.String IsSign
			{
				get
				{
					System.Boolean? data = entity.IsSign;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSign = null;
					else entity.IsSign = Convert.ToBoolean(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String IsUsedForAnalysis
			{
				get
				{
					System.Boolean? data = entity.IsUsedForAnalysis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsedForAnalysis = null;
					else entity.IsUsedForAnalysis = Convert.ToBoolean(value);
				}
			}
			public System.String IsUsedForGuarantorChecklist
			{
				get
				{
					System.Boolean? data = entity.IsUsedForGuarantorChecklist;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsedForGuarantorChecklist = null;
					else entity.IsUsedForGuarantorChecklist = Convert.ToBoolean(value);
				}
			}
			public System.String ProgramID
			{
				get
				{
					System.String data = entity.ProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramID = null;
					else entity.ProgramID = Convert.ToString(value);
				}
			}
			public System.String DocumentInitial
			{
				get
				{
					System.String data = entity.DocumentInitial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentInitial = null;
					else entity.DocumentInitial = Convert.ToString(value);
				}
			}
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
            public System.String SRDocumentFileType
            {
                get
                {
                    System.String data = entity.SRDocumentFileType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDocumentFileType = null;
                    else entity.SRDocumentFileType = Convert.ToString(value);
                }
            }
            public System.String SRAssessmentType
            {
                get
                {
                    System.String data = entity.SRAssessmentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAssessmentType = null;
                    else entity.SRAssessmentType = Convert.ToString(value);
                }
            }
            public System.String SRHaisMonitoring
            {
                get
                {
                    System.String data = entity.SRHaisMonitoring;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRHaisMonitoring = null;
                    else entity.SRHaisMonitoring = Convert.ToString(value);
                }
            }
            private esDocumentFiles entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDocumentFilesQuery query)
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
				throw new Exception("esDocumentFiles can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class DocumentFiles : esDocumentFiles
	{
	}

	[Serializable]
	abstract public class esDocumentFilesQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return DocumentFilesMetadata.Meta();
			}
		}

		public esQueryItem DocumentFilesID
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.DocumentFilesID, esSystemType.Int32);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentNumber
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.DocumentNumber, esSystemType.String);
			}
		}

		public esQueryItem FileTemplateName
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.FileTemplateName, esSystemType.String);
			}
		}

		public esQueryItem IsQuality
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.IsQuality, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLegible
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.IsLegible, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSign
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.IsSign, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsUsedForAnalysis
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.IsUsedForAnalysis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUsedForGuarantorChecklist
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.IsUsedForGuarantorChecklist, esSystemType.Boolean);
			}
		}

		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		}

		public esQueryItem DocumentInitial
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.DocumentInitial, esSystemType.String);
			}
		}

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
        }

        public esQueryItem SRDocumentFileType
        {
            get
            {
                return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.SRDocumentFileType, esSystemType.String);
            }
        }

        public esQueryItem SRAssessmentType
        {
            get
            {
                return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.SRAssessmentType, esSystemType.String);
            }
        }

        public esQueryItem SRHaisMonitoring
        {
            get
            {
                return new esQueryItem(this, DocumentFilesMetadata.ColumnNames.SRHaisMonitoring, esSystemType.String);
            }
        }
    }

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DocumentFilesCollection")]
	public partial class DocumentFilesCollection : esDocumentFilesCollection, IEnumerable<DocumentFiles>
	{
		public DocumentFilesCollection()
		{

		}

		public static implicit operator List<DocumentFiles>(DocumentFilesCollection coll)
		{
			List<DocumentFiles> list = new List<DocumentFiles>();

			foreach (DocumentFiles emp in coll)
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
				return DocumentFilesMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentFilesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DocumentFiles(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DocumentFiles();
		}

		#endregion

		[BrowsableAttribute(false)]
		public DocumentFilesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentFilesQuery();
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
		public bool Load(DocumentFilesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public DocumentFiles AddNew()
		{
			DocumentFiles entity = base.AddNewEntity() as DocumentFiles;

			return entity;
		}
		public DocumentFiles FindByPrimaryKey(Int32 documentFilesID)
		{
			return base.FindByPrimaryKey(documentFilesID) as DocumentFiles;
		}

		#region IEnumerable< DocumentFiles> Members

		IEnumerator<DocumentFiles> IEnumerable<DocumentFiles>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as DocumentFiles;
			}
		}

		#endregion

		private DocumentFilesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DocumentFiles' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("DocumentFiles ({DocumentFilesID})")]
	[Serializable]
	public partial class DocumentFiles : esDocumentFiles
	{
		public DocumentFiles()
		{
		}

		public DocumentFiles(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DocumentFilesMetadata.Meta();
			}
		}

		override protected esDocumentFilesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentFilesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public DocumentFilesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentFilesQuery();
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
		public bool Load(DocumentFilesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private DocumentFilesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class DocumentFilesQuery : esDocumentFilesQuery
	{
		public DocumentFilesQuery()
		{

		}

		public DocumentFilesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "DocumentFilesQuery";
		}
	}

	[Serializable]
	public partial class DocumentFilesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DocumentFilesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.DocumentFilesID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.DocumentFilesID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.DocumentName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.DocumentNumber, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.DocumentNumber;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.FileTemplateName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.FileTemplateName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.IsQuality, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.IsQuality;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.IsLegible, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.IsLegible;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.IsSign, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.IsSign;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.IsUsedForAnalysis, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.IsUsedForAnalysis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.IsUsedForGuarantorChecklist, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.IsUsedForGuarantorChecklist;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.ProgramID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.ProgramID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.DocumentInitial, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.DocumentInitial;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.QuestionFormID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentFilesMetadata.PropertyNames.QuestionFormID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.SRDocumentFileType, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = DocumentFilesMetadata.PropertyNames.SRDocumentFileType;
            c.CharacterMaxLength = 12;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.SRAssessmentType, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = DocumentFilesMetadata.PropertyNames.SRAssessmentType;
            c.CharacterMaxLength = 12;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DocumentFilesMetadata.ColumnNames.SRHaisMonitoring, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = DocumentFilesMetadata.PropertyNames.SRHaisMonitoring;
            c.CharacterMaxLength = 4;
            c.IsNullable = true;
            _columns.Add(c);
        }
		#endregion

		static public DocumentFilesMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string DocumentFilesID = "DocumentFilesID";
			public const string DocumentName = "DocumentName";
			public const string DocumentNumber = "DocumentNumber";
			public const string FileTemplateName = "FileTemplateName";
			public const string IsQuality = "IsQuality";
			public const string IsLegible = "IsLegible";
			public const string IsSign = "IsSign";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUsedForAnalysis = "IsUsedForAnalysis";
			public const string IsUsedForGuarantorChecklist = "IsUsedForGuarantorChecklist";
			public const string ProgramID = "ProgramID";
			public const string DocumentInitial = "DocumentInitial";
			public const string QuestionFormID = "QuestionFormID";
			public const string SRDocumentFileType = "SRDocumentFileType";
			public const string SRAssessmentType = "SRAssessmentType";
			public const string SRHaisMonitoring = "SRHaisMonitoring";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string DocumentFilesID = "DocumentFilesID";
			public const string DocumentName = "DocumentName";
			public const string DocumentNumber = "DocumentNumber";
			public const string FileTemplateName = "FileTemplateName";
			public const string IsQuality = "IsQuality";
			public const string IsLegible = "IsLegible";
			public const string IsSign = "IsSign";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUsedForAnalysis = "IsUsedForAnalysis";
			public const string IsUsedForGuarantorChecklist = "IsUsedForGuarantorChecklist";
			public const string ProgramID = "ProgramID";
			public const string DocumentInitial = "DocumentInitial";
			public const string QuestionFormID = "QuestionFormID";
            public const string SRDocumentFileType = "SRDocumentFileType";
            public const string SRAssessmentType = "SRAssessmentType";
            public const string SRHaisMonitoring = "SRHaisMonitoring";
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
			lock (typeof(DocumentFilesMetadata))
			{
				if (DocumentFilesMetadata.mapDelegates == null)
				{
					DocumentFilesMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (DocumentFilesMetadata.meta == null)
				{
					DocumentFilesMetadata.meta = new DocumentFilesMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("DocumentFilesID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DocumentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FileTemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsQuality", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLegible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSign", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsedForAnalysis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUsedForGuarantorChecklist", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentInitial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDocumentFileType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssessmentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRHaisMonitoring", new esTypeMap("varchar", "System.String"));


				meta.Source = "DocumentFiles";
				meta.Destination = "DocumentFiles";
				meta.spInsert = "proc_DocumentFilesInsert";
				meta.spUpdate = "proc_DocumentFilesUpdate";
				meta.spDelete = "proc_DocumentFilesDelete";
				meta.spLoadAll = "proc_DocumentFilesLoadAll";
				meta.spLoadByPrimaryKey = "proc_DocumentFilesLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DocumentFilesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
