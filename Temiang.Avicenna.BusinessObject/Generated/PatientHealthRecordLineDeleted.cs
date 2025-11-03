/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/2/2019 12:32:28 PM
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
	abstract public class esPatientHealthRecordLineDeletedCollection : esEntityCollectionWAuditLog
	{
		public esPatientHealthRecordLineDeletedCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientHealthRecordLineDeletedCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientHealthRecordLineDeletedQuery query)
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
			this.InitQuery(query as esPatientHealthRecordLineDeletedQuery);
		}
		#endregion
			
		virtual public PatientHealthRecordLineDeleted DetachEntity(PatientHealthRecordLineDeleted entity)
		{
			return base.DetachEntity(entity) as PatientHealthRecordLineDeleted;
		}
		
		virtual public PatientHealthRecordLineDeleted AttachEntity(PatientHealthRecordLineDeleted entity)
		{
			return base.AttachEntity(entity) as PatientHealthRecordLineDeleted;
		}
		
		virtual public void Combine(PatientHealthRecordLineDeletedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientHealthRecordLineDeleted this[int index]
		{
			get
			{
				return base[index] as PatientHealthRecordLineDeleted;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientHealthRecordLineDeleted);
		}
	}

	[Serializable]
	abstract public class esPatientHealthRecordLineDeleted : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientHealthRecordLineDeletedQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientHealthRecordLineDeleted()
		{
		}
	
		public esPatientHealthRecordLineDeleted(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String registrationNo, String questionFormID, String questionGroupID, String questionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, registrationNo, questionFormID, questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, registrationNo, questionFormID, questionGroupID, questionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String registrationNo, String questionFormID, String questionGroupID, String questionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, registrationNo, questionFormID, questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, registrationNo, questionFormID, questionGroupID, questionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String registrationNo, String questionFormID, String questionGroupID, String questionID)
		{
			esPatientHealthRecordLineDeletedQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.RegistrationNo==registrationNo, query.QuestionFormID==questionFormID, query.QuestionGroupID==questionGroupID, query.QuestionID==questionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String registrationNo, String questionFormID, String questionGroupID, String questionID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("QuestionFormID",questionFormID);
			parms.Add("QuestionGroupID",questionGroupID);
			parms.Add("QuestionID",questionID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;
						case "QuestionID": this.str.QuestionID = (string)value; break;
						case "QuestionAnswerPrefix": this.str.QuestionAnswerPrefix = (string)value; break;
						case "QuestionAnswerSuffix": this.str.QuestionAnswerSuffix = (string)value; break;
						case "QuestionAnswerSelectionLineID": this.str.QuestionAnswerSelectionLineID = (string)value; break;
						case "QuestionAnswerText": this.str.QuestionAnswerText = (string)value; break;
						case "QuestionAnswerNum": this.str.QuestionAnswerNum = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "QuestionAnswerText2": this.str.QuestionAnswerText2 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "QuestionAnswerNum":
						
							if (value == null || value is System.Decimal)
								this.QuestionAnswerNum = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "BodyImage":
						
							if (value == null || value is System.Byte[])
								this.BodyImage = (System.Byte[])value;
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
		/// Maps to PatientHealthRecordLineDeleted.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionGroupID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionAnswerPrefix
		/// </summary>
		virtual public System.String QuestionAnswerPrefix
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerPrefix);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerPrefix, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionAnswerSuffix
		/// </summary>
		virtual public System.String QuestionAnswerSuffix
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSuffix);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSuffix, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionAnswerSelectionLineID
		/// </summary>
		virtual public System.String QuestionAnswerSelectionLineID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSelectionLineID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSelectionLineID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionAnswerText
		/// </summary>
		virtual public System.String QuestionAnswerText
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionAnswerNum
		/// </summary>
		virtual public System.Decimal? QuestionAnswerNum
		{
			get
			{
				return base.GetSystemDecimal(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerNum);
			}
			
			set
			{
				base.SetSystemDecimal(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerNum, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.QuestionAnswerText2
		/// </summary>
		virtual public System.String QuestionAnswerText2
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText2);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText2, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordLineDeleted.BodyImage
		/// </summary>
		virtual public System.Byte[] BodyImage
		{
			get
			{
				return base.GetSystemByteArray(PatientHealthRecordLineDeletedMetadata.ColumnNames.BodyImage);
			}
			
			set
			{
				base.SetSystemByteArray(PatientHealthRecordLineDeletedMetadata.ColumnNames.BodyImage, value);
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
			public esStrings(esPatientHealthRecordLineDeleted entity)
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
			public System.String QuestionGroupID
			{
				get
				{
					System.String data = entity.QuestionGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupID = null;
					else entity.QuestionGroupID = Convert.ToString(value);
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
			public System.String QuestionAnswerPrefix
			{
				get
				{
					System.String data = entity.QuestionAnswerPrefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerPrefix = null;
					else entity.QuestionAnswerPrefix = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerSuffix
			{
				get
				{
					System.String data = entity.QuestionAnswerSuffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSuffix = null;
					else entity.QuestionAnswerSuffix = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerSelectionLineID
			{
				get
				{
					System.String data = entity.QuestionAnswerSelectionLineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSelectionLineID = null;
					else entity.QuestionAnswerSelectionLineID = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerText
			{
				get
				{
					System.String data = entity.QuestionAnswerText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerText = null;
					else entity.QuestionAnswerText = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerNum
			{
				get
				{
					System.Decimal? data = entity.QuestionAnswerNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerNum = null;
					else entity.QuestionAnswerNum = Convert.ToDecimal(value);
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
			public System.String QuestionAnswerText2
			{
				get
				{
					System.String data = entity.QuestionAnswerText2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerText2 = null;
					else entity.QuestionAnswerText2 = Convert.ToString(value);
				}
			}
			private esPatientHealthRecordLineDeleted entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientHealthRecordLineDeletedQuery query)
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
				throw new Exception("esPatientHealthRecordLineDeleted can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientHealthRecordLineDeleted : esPatientHealthRecordLineDeleted
	{	
	}

	[Serializable]
	abstract public class esPatientHealthRecordLineDeletedQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientHealthRecordLineDeletedMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerPrefix
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerPrefix, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerSuffix
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSuffix, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerSelectionLineID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSelectionLineID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerText
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerNum
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerNum, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerText2
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText2, esSystemType.String);
			}
		} 
			
		public esQueryItem BodyImage
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordLineDeletedMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientHealthRecordLineDeletedCollection")]
	public partial class PatientHealthRecordLineDeletedCollection : esPatientHealthRecordLineDeletedCollection, IEnumerable< PatientHealthRecordLineDeleted>
	{
		public PatientHealthRecordLineDeletedCollection()
		{

		}	
		
		public static implicit operator List< PatientHealthRecordLineDeleted>(PatientHealthRecordLineDeletedCollection coll)
		{
			List< PatientHealthRecordLineDeleted> list = new List< PatientHealthRecordLineDeleted>();
			
			foreach (PatientHealthRecordLineDeleted emp in coll)
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
				return  PatientHealthRecordLineDeletedMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientHealthRecordLineDeletedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientHealthRecordLineDeleted(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientHealthRecordLineDeleted();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientHealthRecordLineDeletedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientHealthRecordLineDeletedQuery();
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
		public bool Load(PatientHealthRecordLineDeletedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientHealthRecordLineDeleted AddNew()
		{
			PatientHealthRecordLineDeleted entity = base.AddNewEntity() as PatientHealthRecordLineDeleted;
			
			return entity;		
		}
		public PatientHealthRecordLineDeleted FindByPrimaryKey(String transactionNo, String registrationNo, String questionFormID, String questionGroupID, String questionID)
		{
			return base.FindByPrimaryKey(transactionNo, registrationNo, questionFormID, questionGroupID, questionID) as PatientHealthRecordLineDeleted;
		}

		#region IEnumerable< PatientHealthRecordLineDeleted> Members

		IEnumerator< PatientHealthRecordLineDeleted> IEnumerable< PatientHealthRecordLineDeleted>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientHealthRecordLineDeleted;
			}
		}

		#endregion
		
		private PatientHealthRecordLineDeletedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientHealthRecordLineDeleted' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientHealthRecordLineDeleted ({TransactionNo, RegistrationNo, QuestionFormID, QuestionGroupID, QuestionID})")]
	[Serializable]
	public partial class PatientHealthRecordLineDeleted : esPatientHealthRecordLineDeleted
	{
		public PatientHealthRecordLineDeleted()
		{
		}	
	
		public PatientHealthRecordLineDeleted(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientHealthRecordLineDeletedMetadata.Meta();
			}
		}	
	
		override protected esPatientHealthRecordLineDeletedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientHealthRecordLineDeletedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientHealthRecordLineDeletedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientHealthRecordLineDeletedQuery();
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
		public bool Load(PatientHealthRecordLineDeletedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientHealthRecordLineDeletedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientHealthRecordLineDeletedQuery : esPatientHealthRecordLineDeletedQuery
	{
		public PatientHealthRecordLineDeletedQuery()
		{

		}		
		
		public PatientHealthRecordLineDeletedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientHealthRecordLineDeletedQuery";
        }
	}

	[Serializable]
	public partial class PatientHealthRecordLineDeletedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientHealthRecordLineDeletedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionFormID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionGroupID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerPrefix, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionAnswerPrefix;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSuffix, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionAnswerSuffix;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerSelectionLineID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionAnswerSelectionLineID;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionAnswerText;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerNum, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionAnswerNum;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.QuestionAnswerText2, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.QuestionAnswerText2;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordLineDeletedMetadata.ColumnNames.BodyImage, 13, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PatientHealthRecordLineDeletedMetadata.PropertyNames.BodyImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientHealthRecordLineDeletedMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionID = "QuestionID";
			public const string QuestionAnswerPrefix = "QuestionAnswerPrefix";
			public const string QuestionAnswerSuffix = "QuestionAnswerSuffix";
			public const string QuestionAnswerSelectionLineID = "QuestionAnswerSelectionLineID";
			public const string QuestionAnswerText = "QuestionAnswerText";
			public const string QuestionAnswerNum = "QuestionAnswerNum";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string QuestionAnswerText2 = "QuestionAnswerText2";
			public const string BodyImage = "BodyImage";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionID = "QuestionID";
			public const string QuestionAnswerPrefix = "QuestionAnswerPrefix";
			public const string QuestionAnswerSuffix = "QuestionAnswerSuffix";
			public const string QuestionAnswerSelectionLineID = "QuestionAnswerSelectionLineID";
			public const string QuestionAnswerText = "QuestionAnswerText";
			public const string QuestionAnswerNum = "QuestionAnswerNum";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string QuestionAnswerText2 = "QuestionAnswerText2";
			public const string BodyImage = "BodyImage";
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
			lock (typeof(PatientHealthRecordLineDeletedMetadata))
			{
				if(PatientHealthRecordLineDeletedMetadata.mapDelegates == null)
				{
					PatientHealthRecordLineDeletedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientHealthRecordLineDeletedMetadata.meta == null)
				{
					PatientHealthRecordLineDeletedMetadata.meta = new PatientHealthRecordLineDeletedMetadata();
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
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerPrefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerSuffix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerSelectionLineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerNum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerText2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));
		

				meta.Source = "PatientHealthRecordLineDeleted";
				meta.Destination = "PatientHealthRecordLineDeleted";
				meta.spInsert = "proc_PatientHealthRecordLineDeletedInsert";				
				meta.spUpdate = "proc_PatientHealthRecordLineDeletedUpdate";		
				meta.spDelete = "proc_PatientHealthRecordLineDeletedDelete";
				meta.spLoadAll = "proc_PatientHealthRecordLineDeletedLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientHealthRecordLineDeletedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientHealthRecordLineDeletedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
