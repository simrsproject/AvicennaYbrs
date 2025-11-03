/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/19/2021 5:58:24 AM
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
	abstract public class esNursingDiagnosaCollection : esEntityCollectionWAuditLog
	{
		public esNursingDiagnosaCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NursingDiagnosaCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNursingDiagnosaQuery query)
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
			this.InitQuery(query as esNursingDiagnosaQuery);
		}
		#endregion
			
		virtual public NursingDiagnosa DetachEntity(NursingDiagnosa entity)
		{
			return base.DetachEntity(entity) as NursingDiagnosa;
		}
		
		virtual public NursingDiagnosa AttachEntity(NursingDiagnosa entity)
		{
			return base.AttachEntity(entity) as NursingDiagnosa;
		}
		
		virtual public void Combine(NursingDiagnosaCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingDiagnosa this[int index]
		{
			get
			{
				return base[index] as NursingDiagnosa;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingDiagnosa);
		}
	}

	[Serializable]
	abstract public class esNursingDiagnosa : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingDiagnosaQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNursingDiagnosa()
		{
		}
	
		public esNursingDiagnosa(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String nursingDiagnosaID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingDiagnosaID);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingDiagnosaID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String nursingDiagnosaID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingDiagnosaID);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingDiagnosaID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String nursingDiagnosaID)
		{
			esNursingDiagnosaQuery query = this.GetDynamicQuery();
			query.Where(query.NursingDiagnosaID==nursingDiagnosaID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String nursingDiagnosaID)
		{
			esParameters parms = new esParameters();
			parms.Add("NursingDiagnosaID",nursingDiagnosaID);
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
						case "NursingDiagnosaID": this.str.NursingDiagnosaID = (string)value; break;
						case "NursingDiagnosaName": this.str.NursingDiagnosaName = (string)value; break;
						case "SRNursingDiagnosaLevel": this.str.SRNursingDiagnosaLevel = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "NursingDiagnosaParentID": this.str.NursingDiagnosaParentID = (string)value; break;
						case "SRNursingNicType": this.str.SRNursingNicType = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "RespondTemplate": this.str.RespondTemplate = (string)value; break;
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "SRNsDiagnosaType": this.str.SRNsDiagnosaType = (string)value; break;
						case "SRNsEtiologyType": this.str.SRNsEtiologyType = (string)value; break;
						case "Prefix": this.str.Prefix = (string)value; break;
						case "Suffix": this.str.Suffix = (string)value; break;
						case "NursingDiagnosaCode": this.str.NursingDiagnosaCode = (string)value; break;
						case "F1": this.str.F1 = (string)value; break;
						case "F2": this.str.F2 = (string)value; break;
						case "SRNursingNocType": this.str.SRNursingNocType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "TemplateID":
						
							if (value == null || value is System.Int32)
								this.TemplateID = (System.Int32?)value;
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
		/// Maps to NursingDiagnosa.NursingDiagnosaID
		/// </summary>
		virtual public System.String NursingDiagnosaID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.NursingDiagnosaName
		/// </summary>
		virtual public System.String NursingDiagnosaName
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaName);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaName, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.SRNursingDiagnosaLevel
		/// </summary>
		virtual public System.String SRNursingDiagnosaLevel
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNursingDiagnosaLevel);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNursingDiagnosaLevel, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(NursingDiagnosaMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(NursingDiagnosaMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.NursingDiagnosaParentID
		/// </summary>
		virtual public System.String NursingDiagnosaParentID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaParentID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaParentID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.SRNursingNicType
		/// </summary>
		virtual public System.String SRNursingNicType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNursingNicType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNursingNicType, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.RespondTemplate
		/// </summary>
		virtual public System.String RespondTemplate
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.RespondTemplate);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.RespondTemplate, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.TemplateID
		/// </summary>
		virtual public System.Int32? TemplateID
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaMetadata.ColumnNames.TemplateID);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.SRNsDiagnosaType
		/// </summary>
		virtual public System.String SRNsDiagnosaType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNsDiagnosaType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNsDiagnosaType, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.SRNsEtiologyType
		/// </summary>
		virtual public System.String SRNsEtiologyType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNsEtiologyType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNsEtiologyType, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.Prefix
		/// </summary>
		virtual public System.String Prefix
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.Prefix);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.Prefix, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.Suffix
		/// </summary>
		virtual public System.String Suffix
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.Suffix);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.Suffix, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.NursingDiagnosaCode
		/// </summary>
		virtual public System.String NursingDiagnosaCode
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaCode);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaCode, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.F1
		/// </summary>
		virtual public System.String F1
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.F1);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.F1, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.F2
		/// </summary>
		virtual public System.String F2
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.F2);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.F2, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosa.SRNursingNocType
		/// </summary>
		virtual public System.String SRNursingNocType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNursingNocType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaMetadata.ColumnNames.SRNursingNocType, value);
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
			public esStrings(esNursingDiagnosa entity)
			{
				this.entity = entity;
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
			public System.String NursingDiagnosaName
			{
				get
				{
					System.String data = entity.NursingDiagnosaName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaName = null;
					else entity.NursingDiagnosaName = Convert.ToString(value);
				}
			}
			public System.String SRNursingDiagnosaLevel
			{
				get
				{
					System.String data = entity.SRNursingDiagnosaLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNursingDiagnosaLevel = null;
					else entity.SRNursingDiagnosaLevel = Convert.ToString(value);
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
			public System.String NursingDiagnosaParentID
			{
				get
				{
					System.String data = entity.NursingDiagnosaParentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaParentID = null;
					else entity.NursingDiagnosaParentID = Convert.ToString(value);
				}
			}
			public System.String SRNursingNicType
			{
				get
				{
					System.String data = entity.SRNursingNicType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNursingNicType = null;
					else entity.SRNursingNicType = Convert.ToString(value);
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
			public System.String RespondTemplate
			{
				get
				{
					System.String data = entity.RespondTemplate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespondTemplate = null;
					else entity.RespondTemplate = Convert.ToString(value);
				}
			}
			public System.String TemplateID
			{
				get
				{
					System.Int32? data = entity.TemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateID = null;
					else entity.TemplateID = Convert.ToInt32(value);
				}
			}
			public System.String SRNsDiagnosaType
			{
				get
				{
					System.String data = entity.SRNsDiagnosaType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsDiagnosaType = null;
					else entity.SRNsDiagnosaType = Convert.ToString(value);
				}
			}
			public System.String SRNsEtiologyType
			{
				get
				{
					System.String data = entity.SRNsEtiologyType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsEtiologyType = null;
					else entity.SRNsEtiologyType = Convert.ToString(value);
				}
			}
			public System.String Prefix
			{
				get
				{
					System.String data = entity.Prefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Prefix = null;
					else entity.Prefix = Convert.ToString(value);
				}
			}
			public System.String Suffix
			{
				get
				{
					System.String data = entity.Suffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Suffix = null;
					else entity.Suffix = Convert.ToString(value);
				}
			}
			public System.String NursingDiagnosaCode
			{
				get
				{
					System.String data = entity.NursingDiagnosaCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaCode = null;
					else entity.NursingDiagnosaCode = Convert.ToString(value);
				}
			}
			public System.String F1
			{
				get
				{
					System.String data = entity.F1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F1 = null;
					else entity.F1 = Convert.ToString(value);
				}
			}
			public System.String F2
			{
				get
				{
					System.String data = entity.F2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F2 = null;
					else entity.F2 = Convert.ToString(value);
				}
			}
			public System.String SRNursingNocType
			{
				get
				{
					System.String data = entity.SRNursingNocType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNursingNocType = null;
					else entity.SRNursingNocType = Convert.ToString(value);
				}
			}
			private esNursingDiagnosa entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaQuery query)
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
				throw new Exception("esNursingDiagnosa can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NursingDiagnosa : esNursingDiagnosa
	{	
	}

	[Serializable]
	abstract public class esNursingDiagnosaQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaMetadata.Meta();
			}
		}	
			
		public esQueryItem NursingDiagnosaID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID, esSystemType.String);
			}
		} 
			
		public esQueryItem NursingDiagnosaName
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNursingDiagnosaLevel
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.SRNursingDiagnosaLevel, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem NursingDiagnosaParentID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaParentID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNursingNicType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.SRNursingNicType, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RespondTemplate
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.RespondTemplate, esSystemType.String);
			}
		} 
			
		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.TemplateID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRNsDiagnosaType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.SRNsDiagnosaType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNsEtiologyType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.SRNsEtiologyType, esSystemType.String);
			}
		} 
			
		public esQueryItem Prefix
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.Prefix, esSystemType.String);
			}
		} 
			
		public esQueryItem Suffix
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.Suffix, esSystemType.String);
			}
		} 
			
		public esQueryItem NursingDiagnosaCode
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaCode, esSystemType.String);
			}
		} 
			
		public esQueryItem F1
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.F1, esSystemType.String);
			}
		} 
			
		public esQueryItem F2
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.F2, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNursingNocType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaMetadata.ColumnNames.SRNursingNocType, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingDiagnosaCollection")]
	public partial class NursingDiagnosaCollection : esNursingDiagnosaCollection, IEnumerable< NursingDiagnosa>
	{
		public NursingDiagnosaCollection()
		{

		}	
		
		public static implicit operator List< NursingDiagnosa>(NursingDiagnosaCollection coll)
		{
			List< NursingDiagnosa> list = new List< NursingDiagnosa>();
			
			foreach (NursingDiagnosa emp in coll)
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
				return  NursingDiagnosaMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingDiagnosa(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingDiagnosa();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NursingDiagnosaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaQuery();
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
		public bool Load(NursingDiagnosaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NursingDiagnosa AddNew()
		{
			NursingDiagnosa entity = base.AddNewEntity() as NursingDiagnosa;
			
			return entity;		
		}
		public NursingDiagnosa FindByPrimaryKey(String nursingDiagnosaID)
		{
			return base.FindByPrimaryKey(nursingDiagnosaID) as NursingDiagnosa;
		}

		#region IEnumerable< NursingDiagnosa> Members

		IEnumerator< NursingDiagnosa> IEnumerable< NursingDiagnosa>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingDiagnosa;
			}
		}

		#endregion
		
		private NursingDiagnosaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingDiagnosa' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NursingDiagnosa ({NursingDiagnosaID})")]
	[Serializable]
	public partial class NursingDiagnosa : esNursingDiagnosa
	{
		public NursingDiagnosa()
		{
		}	
	
		public NursingDiagnosa(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaMetadata.Meta();
			}
		}	
	
		override protected esNursingDiagnosaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NursingDiagnosaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaQuery();
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
		public bool Load(NursingDiagnosaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NursingDiagnosaQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NursingDiagnosaQuery : esNursingDiagnosaQuery
	{
		public NursingDiagnosaQuery()
		{

		}		
		
		public NursingDiagnosaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NursingDiagnosaQuery";
        }
	}

	[Serializable]
	public partial class NursingDiagnosaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingDiagnosaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.NursingDiagnosaID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.NursingDiagnosaName;
			c.CharacterMaxLength = 450;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.SRNursingDiagnosaLevel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.SRNursingDiagnosaLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.SequenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaParentID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.NursingDiagnosaParentID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.SRNursingNicType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.SRNursingNicType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.RespondTemplate, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.RespondTemplate;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.TemplateID, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.TemplateID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.SRNsDiagnosaType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.SRNsDiagnosaType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.SRNsEtiologyType, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.SRNsEtiologyType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.Prefix, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.Prefix;
			c.CharacterMaxLength = 450;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.Suffix, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.Suffix;
			c.CharacterMaxLength = 450;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaCode, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.NursingDiagnosaCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.F1, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.F1;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.F2, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.F2;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaMetadata.ColumnNames.SRNursingNocType, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaMetadata.PropertyNames.SRNursingNocType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NursingDiagnosaMetadata Meta()
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
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string NursingDiagnosaName = "NursingDiagnosaName";
			public const string SRNursingDiagnosaLevel = "SRNursingDiagnosaLevel";
			public const string SequenceNo = "SequenceNo";
			public const string IsActive = "IsActive";
			public const string NursingDiagnosaParentID = "NursingDiagnosaParentID";
			public const string SRNursingNicType = "SRNursingNicType";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string RespondTemplate = "RespondTemplate";
			public const string TemplateID = "TemplateID";
			public const string SRNsDiagnosaType = "SRNsDiagnosaType";
			public const string SRNsEtiologyType = "SRNsEtiologyType";
			public const string Prefix = "Prefix";
			public const string Suffix = "Suffix";
			public const string NursingDiagnosaCode = "NursingDiagnosaCode";
			public const string F1 = "F1";
			public const string F2 = "F2";
			public const string SRNursingNocType = "SRNursingNocType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string NursingDiagnosaName = "NursingDiagnosaName";
			public const string SRNursingDiagnosaLevel = "SRNursingDiagnosaLevel";
			public const string SequenceNo = "SequenceNo";
			public const string IsActive = "IsActive";
			public const string NursingDiagnosaParentID = "NursingDiagnosaParentID";
			public const string SRNursingNicType = "SRNursingNicType";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string RespondTemplate = "RespondTemplate";
			public const string TemplateID = "TemplateID";
			public const string SRNsDiagnosaType = "SRNsDiagnosaType";
			public const string SRNsEtiologyType = "SRNsEtiologyType";
			public const string Prefix = "Prefix";
			public const string Suffix = "Suffix";
			public const string NursingDiagnosaCode = "NursingDiagnosaCode";
			public const string F1 = "F1";
			public const string F2 = "F2";
			public const string SRNursingNocType = "SRNursingNocType";
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
			lock (typeof(NursingDiagnosaMetadata))
			{
				if(NursingDiagnosaMetadata.mapDelegates == null)
				{
					NursingDiagnosaMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingDiagnosaMetadata.meta == null)
				{
					NursingDiagnosaMetadata.meta = new NursingDiagnosaMetadata();
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
				
				meta.AddTypeMap("NursingDiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingDiagnosaName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNursingDiagnosaLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NursingDiagnosaParentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNursingNicType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RespondTemplate", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRNsDiagnosaType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNsEtiologyType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Prefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Suffix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingDiagnosaCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("F1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("F2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNursingNocType", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "NursingDiagnosa";
				meta.Destination = "NursingDiagnosa";
				meta.spInsert = "proc_NursingDiagnosaInsert";				
				meta.spUpdate = "proc_NursingDiagnosaUpdate";		
				meta.spDelete = "proc_NursingDiagnosaDelete";
				meta.spLoadAll = "proc_NursingDiagnosaLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingDiagnosaLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingDiagnosaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
