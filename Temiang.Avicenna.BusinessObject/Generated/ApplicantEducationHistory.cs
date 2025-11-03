/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esApplicantEducationHistoryCollection : esEntityCollectionWAuditLog
	{
		public esApplicantEducationHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantEducationHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantEducationHistoryQuery query)
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
			this.InitQuery(query as esApplicantEducationHistoryQuery);
		}
		#endregion
		
		virtual public ApplicantEducationHistory DetachEntity(ApplicantEducationHistory entity)
		{
			return base.DetachEntity(entity) as ApplicantEducationHistory;
		}
		
		virtual public ApplicantEducationHistory AttachEntity(ApplicantEducationHistory entity)
		{
			return base.AttachEntity(entity) as ApplicantEducationHistory;
		}
		
		virtual public void Combine(ApplicantEducationHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantEducationHistory this[int index]
		{
			get
			{
				return base[index] as ApplicantEducationHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantEducationHistory);
		}
	}



	[Serializable]
	abstract public class esApplicantEducationHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantEducationHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantEducationHistory()
		{

		}

		public esApplicantEducationHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantEducationHistoryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantEducationHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantEducationHistoryID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantEducationHistoryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantEducationHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantEducationHistoryID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantEducationHistoryID)
		{
			esApplicantEducationHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantEducationHistoryID == applicantEducationHistoryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantEducationHistoryID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantEducationHistoryID",applicantEducationHistoryID);
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
						case "ApplicantEducationHistoryID": this.str.ApplicantEducationHistoryID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;							
						case "InstitutionName": this.str.InstitutionName = (string)value; break;							
						case "Location": this.str.Location = (string)value; break;							
						case "StartYear": this.str.StartYear = (string)value; break;							
						case "EndYear": this.str.EndYear = (string)value; break;							
						case "Gpa": this.str.Gpa = (string)value; break;							
						case "Achievement": this.str.Achievement = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantEducationHistoryID":
						
							if (value == null || value is System.Int32)
								this.ApplicantEducationHistoryID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
							break;
						
						case "Gpa":
						
							if (value == null || value is System.Decimal)
								this.Gpa = (System.Decimal?)value;
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
		/// Maps to ApplicantEducationHistory.ApplicantEducationHistoryID
		/// </summary>
		virtual public System.Int32? ApplicantEducationHistoryID
		{
			get
			{
				return base.GetSystemInt32(ApplicantEducationHistoryMetadata.ColumnNames.ApplicantEducationHistoryID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantEducationHistoryMetadata.ColumnNames.ApplicantEducationHistoryID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantEducationHistoryMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantEducationHistoryMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.SREducationLevel);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.InstitutionName
		/// </summary>
		virtual public System.String InstitutionName
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.InstitutionName);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.InstitutionName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.Location
		/// </summary>
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.Location);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.Location, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.StartYear
		/// </summary>
		virtual public System.String StartYear
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.StartYear);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.StartYear, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.EndYear
		/// </summary>
		virtual public System.String EndYear
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.EndYear);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.EndYear, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.Gpa
		/// </summary>
		virtual public System.Decimal? Gpa
		{
			get
			{
				return base.GetSystemDecimal(ApplicantEducationHistoryMetadata.ColumnNames.Gpa);
			}
			
			set
			{
				base.SetSystemDecimal(ApplicantEducationHistoryMetadata.ColumnNames.Gpa, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.Achievement
		/// </summary>
		virtual public System.String Achievement
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.Achievement);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.Achievement, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantEducationHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esApplicantEducationHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantEducationHistoryID
			{
				get
				{
					System.Int32? data = entity.ApplicantEducationHistoryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantEducationHistoryID = null;
					else entity.ApplicantEducationHistoryID = Convert.ToInt32(value);
				}
			}
				
			public System.String ApplicantID
			{
				get
				{
					System.Int32? data = entity.ApplicantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantID = null;
					else entity.ApplicantID = Convert.ToInt32(value);
				}
			}
				
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
				
			public System.String InstitutionName
			{
				get
				{
					System.String data = entity.InstitutionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstitutionName = null;
					else entity.InstitutionName = Convert.ToString(value);
				}
			}
				
			public System.String Location
			{
				get
				{
					System.String data = entity.Location;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Location = null;
					else entity.Location = Convert.ToString(value);
				}
			}
				
			public System.String StartYear
			{
				get
				{
					System.String data = entity.StartYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartYear = null;
					else entity.StartYear = Convert.ToString(value);
				}
			}
				
			public System.String EndYear
			{
				get
				{
					System.String data = entity.EndYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndYear = null;
					else entity.EndYear = Convert.ToString(value);
				}
			}
				
			public System.String Gpa
			{
				get
				{
					System.Decimal? data = entity.Gpa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Gpa = null;
					else entity.Gpa = Convert.ToDecimal(value);
				}
			}
				
			public System.String Achievement
			{
				get
				{
					System.String data = entity.Achievement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Achievement = null;
					else entity.Achievement = Convert.ToString(value);
				}
			}
				
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			

			private esApplicantEducationHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantEducationHistoryQuery query)
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
				throw new Exception("esApplicantEducationHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantEducationHistory : esApplicantEducationHistory
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esApplicantEducationHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantEducationHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantEducationHistoryID
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.ApplicantEducationHistoryID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem InstitutionName
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.InstitutionName, esSystemType.String);
			}
		} 
		
		public esQueryItem Location
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.Location, esSystemType.String);
			}
		} 
		
		public esQueryItem StartYear
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.StartYear, esSystemType.String);
			}
		} 
		
		public esQueryItem EndYear
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.EndYear, esSystemType.String);
			}
		} 
		
		public esQueryItem Gpa
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.Gpa, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Achievement
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.Achievement, esSystemType.String);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantEducationHistoryCollection")]
	public partial class ApplicantEducationHistoryCollection : esApplicantEducationHistoryCollection, IEnumerable<ApplicantEducationHistory>
	{
		public ApplicantEducationHistoryCollection()
		{

		}
		
		public static implicit operator List<ApplicantEducationHistory>(ApplicantEducationHistoryCollection coll)
		{
			List<ApplicantEducationHistory> list = new List<ApplicantEducationHistory>();
			
			foreach (ApplicantEducationHistory emp in coll)
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
				return  ApplicantEducationHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantEducationHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantEducationHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantEducationHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantEducationHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantEducationHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantEducationHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantEducationHistory AddNew()
		{
			ApplicantEducationHistory entity = base.AddNewEntity() as ApplicantEducationHistory;
			
			return entity;
		}

		public ApplicantEducationHistory FindByPrimaryKey(System.Int32 applicantEducationHistoryID)
		{
			return base.FindByPrimaryKey(applicantEducationHistoryID) as ApplicantEducationHistory;
		}


		#region IEnumerable<ApplicantEducationHistory> Members

		IEnumerator<ApplicantEducationHistory> IEnumerable<ApplicantEducationHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantEducationHistory;
			}
		}

		#endregion
		
		private ApplicantEducationHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantEducationHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantEducationHistory ({ApplicantEducationHistoryID})")]
	[Serializable]
	public partial class ApplicantEducationHistory : esApplicantEducationHistory
	{
		public ApplicantEducationHistory()
		{

		}
	
		public ApplicantEducationHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantEducationHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantEducationHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantEducationHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantEducationHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantEducationHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantEducationHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantEducationHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantEducationHistoryQuery : esApplicantEducationHistoryQuery
	{
		public ApplicantEducationHistoryQuery()
		{

		}		
		
		public ApplicantEducationHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantEducationHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantEducationHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantEducationHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.ApplicantEducationHistoryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.ApplicantEducationHistoryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.SREducationLevel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.InstitutionName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.InstitutionName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.Location, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.StartYear, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.StartYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.EndYear, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.EndYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.Gpa, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.Gpa;
			c.NumericPrecision = 3;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.Achievement, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.Achievement;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.Note, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantEducationHistoryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantEducationHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantEducationHistoryMetadata Meta()
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
			 public const string ApplicantEducationHistoryID = "ApplicantEducationHistoryID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string InstitutionName = "InstitutionName";
			 public const string Location = "Location";
			 public const string StartYear = "StartYear";
			 public const string EndYear = "EndYear";
			 public const string Gpa = "Gpa";
			 public const string Achievement = "Achievement";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantEducationHistoryID = "ApplicantEducationHistoryID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string InstitutionName = "InstitutionName";
			 public const string Location = "Location";
			 public const string StartYear = "StartYear";
			 public const string EndYear = "EndYear";
			 public const string Gpa = "Gpa";
			 public const string Achievement = "Achievement";
			 public const string Note = "Note";
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
			lock (typeof(ApplicantEducationHistoryMetadata))
			{
				if(ApplicantEducationHistoryMetadata.mapDelegates == null)
				{
					ApplicantEducationHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantEducationHistoryMetadata.meta == null)
				{
					ApplicantEducationHistoryMetadata.meta = new ApplicantEducationHistoryMetadata();
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
				

				meta.AddTypeMap("ApplicantEducationHistoryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstitutionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartYear", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndYear", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Gpa", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Achievement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantEducationHistory";
				meta.Destination = "ApplicantEducationHistory";
				
				meta.spInsert = "proc_ApplicantEducationHistoryInsert";				
				meta.spUpdate = "proc_ApplicantEducationHistoryUpdate";		
				meta.spDelete = "proc_ApplicantEducationHistoryDelete";
				meta.spLoadAll = "proc_ApplicantEducationHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantEducationHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantEducationHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
