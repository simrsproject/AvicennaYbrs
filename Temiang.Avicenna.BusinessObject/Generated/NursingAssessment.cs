/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 5/27/2015 7:45:48 AM
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
	abstract public class esNursingAssessmentCollection : esEntityCollection
	{
		public esNursingAssessmentCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NursingAssessmentCollection";
		}

		#region Query Logic
		protected void InitQuery(esNursingAssessmentQuery query)
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
			this.InitQuery(query as esNursingAssessmentQuery);
		}
		#endregion
		
		virtual public NursingAssessment DetachEntity(NursingAssessment entity)
		{
			return base.DetachEntity(entity) as NursingAssessment;
		}
		
		virtual public NursingAssessment AttachEntity(NursingAssessment entity)
		{
			return base.AttachEntity(entity) as NursingAssessment;
		}
		
		virtual public void Combine(NursingAssessmentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingAssessment this[int index]
		{
			get
			{
				return base[index] as NursingAssessment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingAssessment);
		}
	}



	[Serializable]
	abstract public class esNursingAssessment : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingAssessmentQuery GetDynamicQuery()
		{
			return null;
		}

		public esNursingAssessment()
		{

		}

		public esNursingAssessment(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String nursingAssessmentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingAssessmentID);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingAssessmentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String nursingAssessmentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingAssessmentID);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingAssessmentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String nursingAssessmentID)
		{
			esNursingAssessmentQuery query = this.GetDynamicQuery();
			query.Where(query.NursingAssessmentID == nursingAssessmentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String nursingAssessmentID)
		{
			esParameters parms = new esParameters();
			parms.Add("NursingAssessmentID",nursingAssessmentID);
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
						case "NursingAssessmentID": this.str.NursingAssessmentID = (string)value; break;							
						case "NursingAssessmentName": this.str.NursingAssessmentName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "IsSubjective": this.str.IsSubjective = (string)value; break;							
						case "IsObjective": this.str.IsObjective = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "QuestionID": this.str.QuestionID = (string)value; break;							
						case "SRAnswerType": this.str.SRAnswerType = (string)value; break;							
						case "AnswerDecimalDigit": this.str.AnswerDecimalDigit = (string)value; break;							
						case "AnswerPrefix": this.str.AnswerPrefix = (string)value; break;							
						case "AnswerSuffix": this.str.AnswerSuffix = (string)value; break;
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
						
						case "IsSubjective":
						
							if (value == null || value is System.Boolean)
								this.IsSubjective = (System.Boolean?)value;
							break;
						
						case "IsObjective":
						
							if (value == null || value is System.Boolean)
								this.IsObjective = (System.Boolean?)value;
							break;
						
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "AnswerDecimalDigit":
						
							if (value == null || value is System.Int32)
								this.AnswerDecimalDigit = (System.Int32?)value;
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
		/// Maps to NursingAssessment.NursingAssessmentID
		/// </summary>
		virtual public System.String NursingAssessmentID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.NursingAssessmentID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.NursingAssessmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.NursingAssessmentName
		/// </summary>
		virtual public System.String NursingAssessmentName
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.NursingAssessmentName);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.NursingAssessmentName, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.IsSubjective
		/// </summary>
		virtual public System.Boolean? IsSubjective
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentMetadata.ColumnNames.IsSubjective);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentMetadata.ColumnNames.IsSubjective, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.IsObjective
		/// </summary>
		virtual public System.Boolean? IsObjective
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentMetadata.ColumnNames.IsObjective);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentMetadata.ColumnNames.IsObjective, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.QuestionID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.SRAnswerType
		/// </summary>
		virtual public System.String SRAnswerType
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.SRAnswerType);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.SRAnswerType, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.AnswerDecimalDigit
		/// </summary>
		virtual public System.Int32? AnswerDecimalDigit
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentMetadata.ColumnNames.AnswerDecimalDigit);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentMetadata.ColumnNames.AnswerDecimalDigit, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.AnswerPrefix
		/// </summary>
		virtual public System.String AnswerPrefix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.AnswerPrefix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.AnswerPrefix, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessment.AnswerSuffix
		/// </summary>
		virtual public System.String AnswerSuffix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentMetadata.ColumnNames.AnswerSuffix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentMetadata.ColumnNames.AnswerSuffix, value);
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
			public esStrings(esNursingAssessment entity)
			{
				this.entity = entity;
			}
			
	
			public System.String NursingAssessmentID
			{
				get
				{
					System.String data = entity.NursingAssessmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingAssessmentID = null;
					else entity.NursingAssessmentID = Convert.ToString(value);
				}
			}
				
			public System.String NursingAssessmentName
			{
				get
				{
					System.String data = entity.NursingAssessmentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingAssessmentName = null;
					else entity.NursingAssessmentName = Convert.ToString(value);
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
				
			public System.String IsSubjective
			{
				get
				{
					System.Boolean? data = entity.IsSubjective;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSubjective = null;
					else entity.IsSubjective = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsObjective
			{
				get
				{
					System.Boolean? data = entity.IsObjective;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsObjective = null;
					else entity.IsObjective = Convert.ToBoolean(value);
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
				
			public System.String AnswerDecimalDigit
			{
				get
				{
					System.Int32? data = entity.AnswerDecimalDigit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerDecimalDigit = null;
					else entity.AnswerDecimalDigit = Convert.ToInt32(value);
				}
			}
				
			public System.String AnswerPrefix
			{
				get
				{
					System.String data = entity.AnswerPrefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerPrefix = null;
					else entity.AnswerPrefix = Convert.ToString(value);
				}
			}
				
			public System.String AnswerSuffix
			{
				get
				{
					System.String data = entity.AnswerSuffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerSuffix = null;
					else entity.AnswerSuffix = Convert.ToString(value);
				}
			}
			

			private esNursingAssessment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingAssessmentQuery query)
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
				throw new Exception("esNursingAssessment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNursingAssessmentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentMetadata.Meta();
			}
		}	
		

		public esQueryItem NursingAssessmentID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.NursingAssessmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem NursingAssessmentName
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.NursingAssessmentName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsSubjective
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.IsSubjective, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsObjective
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.IsObjective, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAnswerType
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.SRAnswerType, esSystemType.String);
			}
		} 
		
		public esQueryItem AnswerDecimalDigit
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.AnswerDecimalDigit, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AnswerPrefix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.AnswerPrefix, esSystemType.String);
			}
		} 
		
		public esQueryItem AnswerSuffix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentMetadata.ColumnNames.AnswerSuffix, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingAssessmentCollection")]
	public partial class NursingAssessmentCollection : esNursingAssessmentCollection, IEnumerable<NursingAssessment>
	{
		public NursingAssessmentCollection()
		{

		}
		
		public static implicit operator List<NursingAssessment>(NursingAssessmentCollection coll)
		{
			List<NursingAssessment> list = new List<NursingAssessment>();
			
			foreach (NursingAssessment emp in coll)
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
				return  NursingAssessmentMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingAssessment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingAssessment();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NursingAssessmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NursingAssessmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NursingAssessment AddNew()
		{
			NursingAssessment entity = base.AddNewEntity() as NursingAssessment;
			
			return entity;
		}

		public NursingAssessment FindByPrimaryKey(System.String nursingAssessmentID)
		{
			return base.FindByPrimaryKey(nursingAssessmentID) as NursingAssessment;
		}


		#region IEnumerable<NursingAssessment> Members

		IEnumerator<NursingAssessment> IEnumerable<NursingAssessment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingAssessment;
			}
		}

		#endregion
		
		private NursingAssessmentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingAssessment' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NursingAssessment ({NursingAssessmentID})")]
	[Serializable]
	public partial class NursingAssessment : esNursingAssessment
	{
		public NursingAssessment()
		{

		}
	
		public NursingAssessment(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentMetadata.Meta();
			}
		}
		
		
		
		override protected esNursingAssessmentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NursingAssessmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NursingAssessmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NursingAssessmentQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NursingAssessmentQuery : esNursingAssessmentQuery
	{
		public NursingAssessmentQuery()
		{

		}		
		
		public NursingAssessmentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NursingAssessmentQuery";
        }
		
			
	}


	[Serializable]
	public partial class NursingAssessmentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingAssessmentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.NursingAssessmentID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.NursingAssessmentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.NursingAssessmentName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.NursingAssessmentName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.IsSubjective, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.IsSubjective;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.IsObjective, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.IsObjective;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.QuestionID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.QuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.SRAnswerType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.SRAnswerType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.AnswerDecimalDigit, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.AnswerDecimalDigit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.AnswerPrefix, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.AnswerPrefix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentMetadata.ColumnNames.AnswerSuffix, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentMetadata.PropertyNames.AnswerSuffix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NursingAssessmentMetadata Meta()
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
			 public const string NursingAssessmentID = "NursingAssessmentID";
			 public const string NursingAssessmentName = "NursingAssessmentName";
			 public const string IsActive = "IsActive";
			 public const string IsSubjective = "IsSubjective";
			 public const string IsObjective = "IsObjective";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string QuestionID = "QuestionID";
			 public const string SRAnswerType = "SRAnswerType";
			 public const string AnswerDecimalDigit = "AnswerDecimalDigit";
			 public const string AnswerPrefix = "AnswerPrefix";
			 public const string AnswerSuffix = "AnswerSuffix";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NursingAssessmentID = "NursingAssessmentID";
			 public const string NursingAssessmentName = "NursingAssessmentName";
			 public const string IsActive = "IsActive";
			 public const string IsSubjective = "IsSubjective";
			 public const string IsObjective = "IsObjective";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string QuestionID = "QuestionID";
			 public const string SRAnswerType = "SRAnswerType";
			 public const string AnswerDecimalDigit = "AnswerDecimalDigit";
			 public const string AnswerPrefix = "AnswerPrefix";
			 public const string AnswerSuffix = "AnswerSuffix";
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
			lock (typeof(NursingAssessmentMetadata))
			{
				if(NursingAssessmentMetadata.mapDelegates == null)
				{
					NursingAssessmentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingAssessmentMetadata.meta == null)
				{
					NursingAssessmentMetadata.meta = new NursingAssessmentMetadata();
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
				

				meta.AddTypeMap("NursingAssessmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingAssessmentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSubjective", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsObjective", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAnswerType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerDecimalDigit", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AnswerPrefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerSuffix", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "NursingAssessment";
				meta.Destination = "NursingAssessment";
				
				meta.spInsert = "proc_NursingAssessmentInsert";				
				meta.spUpdate = "proc_NursingAssessmentUpdate";		
				meta.spDelete = "proc_NursingAssessmentDelete";
				meta.spLoadAll = "proc_NursingAssessmentLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingAssessmentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingAssessmentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
