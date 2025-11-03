/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/24/2021 5:45:38 PM
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
	abstract public class esResearchLetterCollection : esEntityCollectionWAuditLog
	{
		public esResearchLetterCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ResearchLetterCollection";
		}

		#region Query Logic
		protected void InitQuery(esResearchLetterQuery query)
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
			this.InitQuery(query as esResearchLetterQuery);
		}
		#endregion

		virtual public ResearchLetter DetachEntity(ResearchLetter entity)
		{
			return base.DetachEntity(entity) as ResearchLetter;
		}

		virtual public ResearchLetter AttachEntity(ResearchLetter entity)
		{
			return base.AttachEntity(entity) as ResearchLetter;
		}

		virtual public void Combine(ResearchLetterCollection collection)
		{
			base.Combine(collection);
		}

		new public ResearchLetter this[int index]
		{
			get
			{
				return base[index] as ResearchLetter;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ResearchLetter);
		}
	}

	[Serializable]
	abstract public class esResearchLetter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esResearchLetterQuery GetDynamicQuery()
		{
			return null;
		}

		public esResearchLetter()
		{
		}

		public esResearchLetter(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 letterID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(letterID);
			else
				return LoadByPrimaryKeyStoredProcedure(letterID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 letterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(letterID);
			else
				return LoadByPrimaryKeyStoredProcedure(letterID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 letterID)
		{
			esResearchLetterQuery query = this.GetDynamicQuery();
			query.Where(query.LetterID == letterID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 letterID)
		{
			esParameters parms = new esParameters();
			parms.Add("LetterID", letterID);
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
						case "LetterID": this.str.LetterID = (string)value; break;
						case "ResearcherName": this.str.ResearcherName = (string)value; break;
						case "LetterNo": this.str.LetterNo = (string)value; break;
						case "LetterDate": this.str.LetterDate = (string)value; break;
						case "Subject": this.str.Subject = (string)value; break;
						case "SRResearchDecision": this.str.SRResearchDecision = (string)value; break;
						case "Attachment": this.str.Attachment = (string)value; break;
						case "SRResearchInstitution": this.str.SRResearchInstitution = (string)value; break;
						case "SRResearchFaculty": this.str.SRResearchFaculty = (string)value; break;
						case "SRResearchMajors": this.str.SRResearchMajors = (string)value; break;
						case "SREducationDegree": this.str.SREducationDegree = (string)value; break;
						case "IsUpload": this.str.IsUpload = (string)value; break;
						case "ReviewTime": this.str.ReviewTime = (string)value; break;
						case "SRResearchReviewerName": this.str.SRResearchReviewerName = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LetterID":

							if (value == null || value is System.Int64)
								this.LetterID = (System.Int64?)value;
							break;
						case "LetterDate":

							if (value == null || value is System.DateTime)
								this.LetterDate = (System.DateTime?)value;
							break;
						case "IsUpload":

							if (value == null || value is System.Boolean)
								this.IsUpload = (System.Boolean?)value;
							break;
						case "ReviewTime":

							if (value == null || value is System.Int16)
								this.ReviewTime = (System.Int16?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to ResearchLetter.LetterID
		/// </summary>
		virtual public System.Int64? LetterID
		{
			get
			{
				return base.GetSystemInt64(ResearchLetterMetadata.ColumnNames.LetterID);
			}

			set
			{
				base.SetSystemInt64(ResearchLetterMetadata.ColumnNames.LetterID, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.ResearcherName
		/// </summary>
		virtual public System.String ResearcherName
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.ResearcherName);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.ResearcherName, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.LetterNo
		/// </summary>
		virtual public System.String LetterNo
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.LetterNo);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.LetterNo, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.LetterDate
		/// </summary>
		virtual public System.DateTime? LetterDate
		{
			get
			{
				return base.GetSystemDateTime(ResearchLetterMetadata.ColumnNames.LetterDate);
			}

			set
			{
				base.SetSystemDateTime(ResearchLetterMetadata.ColumnNames.LetterDate, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.Subject
		/// </summary>
		virtual public System.String Subject
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.Subject);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.Subject, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.SRResearchDecision
		/// </summary>
		virtual public System.String SRResearchDecision
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchDecision);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchDecision, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.Attachment
		/// </summary>
		virtual public System.String Attachment
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.Attachment);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.Attachment, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.SRResearchInstitution
		/// </summary>
		virtual public System.String SRResearchInstitution
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchInstitution);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchInstitution, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.SRResearchFaculty
		/// </summary>
		virtual public System.String SRResearchFaculty
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchFaculty);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchFaculty, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.SRResearchMajors
		/// </summary>
		virtual public System.String SRResearchMajors
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchMajors);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchMajors, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.SREducationDegree
		/// </summary>
		virtual public System.String SREducationDegree
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.SREducationDegree);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.SREducationDegree, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(ResearchLetterMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(ResearchLetterMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.ReviewTime
		/// </summary>
		virtual public System.Int16? ReviewTime
		{
			get
			{
				return base.GetSystemInt16(ResearchLetterMetadata.ColumnNames.ReviewTime);
			}

			set
			{
				base.SetSystemInt16(ResearchLetterMetadata.ColumnNames.ReviewTime, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.SRResearchReviewerName
		/// </summary>
		virtual public System.String SRResearchReviewerName
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchReviewerName);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.SRResearchReviewerName, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ResearchLetterMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ResearchLetterMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ResearchLetterMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(ResearchLetterMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ResearchLetterMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ResearchLetterMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ResearchLetterMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ResearchLetterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetter.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ResearchLetterMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ResearchLetterMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esResearchLetter entity)
			{
				this.entity = entity;
			}
			public System.String LetterID
			{
				get
				{
					System.Int64? data = entity.LetterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterID = null;
					else entity.LetterID = Convert.ToInt64(value);
				}
			}
			public System.String ResearcherName
			{
				get
				{
					System.String data = entity.ResearcherName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResearcherName = null;
					else entity.ResearcherName = Convert.ToString(value);
				}
			}
			public System.String LetterNo
			{
				get
				{
					System.String data = entity.LetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterNo = null;
					else entity.LetterNo = Convert.ToString(value);
				}
			}
			public System.String LetterDate
			{
				get
				{
					System.DateTime? data = entity.LetterDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterDate = null;
					else entity.LetterDate = Convert.ToDateTime(value);
				}
			}
			public System.String Subject
			{
				get
				{
					System.String data = entity.Subject;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Subject = null;
					else entity.Subject = Convert.ToString(value);
				}
			}
			public System.String SRResearchDecision
			{
				get
				{
					System.String data = entity.SRResearchDecision;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResearchDecision = null;
					else entity.SRResearchDecision = Convert.ToString(value);
				}
			}
			public System.String Attachment
			{
				get
				{
					System.String data = entity.Attachment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attachment = null;
					else entity.Attachment = Convert.ToString(value);
				}
			}
			public System.String SRResearchInstitution
			{
				get
				{
					System.String data = entity.SRResearchInstitution;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResearchInstitution = null;
					else entity.SRResearchInstitution = Convert.ToString(value);
				}
			}
			public System.String SRResearchFaculty
			{
				get
				{
					System.String data = entity.SRResearchFaculty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResearchFaculty = null;
					else entity.SRResearchFaculty = Convert.ToString(value);
				}
			}
			public System.String SRResearchMajors
			{
				get
				{
					System.String data = entity.SRResearchMajors;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResearchMajors = null;
					else entity.SRResearchMajors = Convert.ToString(value);
				}
			}
			public System.String SREducationDegree
			{
				get
				{
					System.String data = entity.SREducationDegree;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationDegree = null;
					else entity.SREducationDegree = Convert.ToString(value);
				}
			}
			public System.String IsUpload
			{
				get
				{
					System.Boolean? data = entity.IsUpload;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUpload = null;
					else entity.IsUpload = Convert.ToBoolean(value);
				}
			}
			public System.String ReviewTime
			{
				get
				{
					System.Int16? data = entity.ReviewTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReviewTime = null;
					else entity.ReviewTime = Convert.ToInt16(value);
				}
			}
			public System.String SRResearchReviewerName
			{
				get
				{
					System.String data = entity.SRResearchReviewerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResearchReviewerName = null;
					else entity.SRResearchReviewerName = Convert.ToString(value);
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
			private esResearchLetter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esResearchLetterQuery query)
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
				throw new Exception("esResearchLetter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ResearchLetter : esResearchLetter
	{
	}

	[Serializable]
	abstract public class esResearchLetterQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ResearchLetterMetadata.Meta();
			}
		}

		public esQueryItem LetterID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.LetterID, esSystemType.Int64);
			}
		}

		public esQueryItem ResearcherName
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.ResearcherName, esSystemType.String);
			}
		}

		public esQueryItem LetterNo
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.LetterNo, esSystemType.String);
			}
		}

		public esQueryItem LetterDate
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.LetterDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Subject
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.Subject, esSystemType.String);
			}
		}

		public esQueryItem SRResearchDecision
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.SRResearchDecision, esSystemType.String);
			}
		}

		public esQueryItem Attachment
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.Attachment, esSystemType.String);
			}
		}

		public esQueryItem SRResearchInstitution
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.SRResearchInstitution, esSystemType.String);
			}
		}

		public esQueryItem SRResearchFaculty
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.SRResearchFaculty, esSystemType.String);
			}
		}

		public esQueryItem SRResearchMajors
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.SRResearchMajors, esSystemType.String);
			}
		}

		public esQueryItem SREducationDegree
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.SREducationDegree, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem ReviewTime
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.ReviewTime, esSystemType.Int16);
			}
		}

		public esQueryItem SRResearchReviewerName
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.SRResearchReviewerName, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ResearchLetterCollection")]
	public partial class ResearchLetterCollection : esResearchLetterCollection, IEnumerable<ResearchLetter>
	{
		public ResearchLetterCollection()
		{

		}

		public static implicit operator List<ResearchLetter>(ResearchLetterCollection coll)
		{
			List<ResearchLetter> list = new List<ResearchLetter>();

			foreach (ResearchLetter emp in coll)
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
				return ResearchLetterMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ResearchLetterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ResearchLetter(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ResearchLetter();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ResearchLetterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ResearchLetterQuery();
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
		public bool Load(ResearchLetterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ResearchLetter AddNew()
		{
			ResearchLetter entity = base.AddNewEntity() as ResearchLetter;

			return entity;
		}
		public ResearchLetter FindByPrimaryKey(Int64 letterID)
		{
			return base.FindByPrimaryKey(letterID) as ResearchLetter;
		}

		#region IEnumerable< ResearchLetter> Members

		IEnumerator<ResearchLetter> IEnumerable<ResearchLetter>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ResearchLetter;
			}
		}

		#endregion

		private ResearchLetterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ResearchLetter' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ResearchLetter ({LetterID})")]
	[Serializable]
	public partial class ResearchLetter : esResearchLetter
	{
		public ResearchLetter()
		{
		}

		public ResearchLetter(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ResearchLetterMetadata.Meta();
			}
		}

		override protected esResearchLetterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ResearchLetterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ResearchLetterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ResearchLetterQuery();
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
		public bool Load(ResearchLetterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ResearchLetterQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ResearchLetterQuery : esResearchLetterQuery
	{
		public ResearchLetterQuery()
		{

		}

		public ResearchLetterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ResearchLetterQuery";
		}
	}

	[Serializable]
	public partial class ResearchLetterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ResearchLetterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.LetterID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.LetterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.ResearcherName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.ResearcherName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.LetterNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.LetterNo;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.LetterDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.LetterDate;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.Subject, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.Subject;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.SRResearchDecision, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.SRResearchDecision;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.Attachment, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.Attachment;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.SRResearchInstitution, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.SRResearchInstitution;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.SRResearchFaculty, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.SRResearchFaculty;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.SRResearchMajors, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.SRResearchMajors;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.SREducationDegree, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.SREducationDegree;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.IsUpload, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.IsUpload;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.ReviewTime, 12, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.ReviewTime;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.SRResearchReviewerName, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.SRResearchReviewerName;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.VoidDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.VoidByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.CreatedDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.CreatedByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ResearchLetterMetadata Meta()
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
			public const string LetterID = "LetterID";
			public const string ResearcherName = "ResearcherName";
			public const string LetterNo = "LetterNo";
			public const string LetterDate = "LetterDate";
			public const string Subject = "Subject";
			public const string SRResearchDecision = "SRResearchDecision";
			public const string Attachment = "Attachment";
			public const string SRResearchInstitution = "SRResearchInstitution";
			public const string SRResearchFaculty = "SRResearchFaculty";
			public const string SRResearchMajors = "SRResearchMajors";
			public const string SREducationDegree = "SREducationDegree";
			public const string IsUpload = "IsUpload";
			public const string ReviewTime = "ReviewTime";
			public const string SRResearchReviewerName = "SRResearchReviewerName";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LetterID = "LetterID";
			public const string ResearcherName = "ResearcherName";
			public const string LetterNo = "LetterNo";
			public const string LetterDate = "LetterDate";
			public const string Subject = "Subject";
			public const string SRResearchDecision = "SRResearchDecision";
			public const string Attachment = "Attachment";
			public const string SRResearchInstitution = "SRResearchInstitution";
			public const string SRResearchFaculty = "SRResearchFaculty";
			public const string SRResearchMajors = "SRResearchMajors";
			public const string SREducationDegree = "SREducationDegree";
			public const string IsUpload = "IsUpload";
			public const string ReviewTime = "ReviewTime";
			public const string SRResearchReviewerName = "SRResearchReviewerName";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(ResearchLetterMetadata))
			{
				if (ResearchLetterMetadata.mapDelegates == null)
				{
					ResearchLetterMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ResearchLetterMetadata.meta == null)
				{
					ResearchLetterMetadata.meta = new ResearchLetterMetadata();
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

				meta.AddTypeMap("LetterID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ResearcherName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LetterDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Subject", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRResearchDecision", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Attachment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRResearchInstitution", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRResearchFaculty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRResearchMajors", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationDegree", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUpload", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReviewTime", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SRResearchReviewerName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ResearchLetter";
				meta.Destination = "ResearchLetter";
				meta.spInsert = "proc_ResearchLetterInsert";
				meta.spUpdate = "proc_ResearchLetterUpdate";
				meta.spDelete = "proc_ResearchLetterDelete";
				meta.spLoadAll = "proc_ResearchLetterLoadAll";
				meta.spLoadByPrimaryKey = "proc_ResearchLetterLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ResearchLetterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
