/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/7/2014 11:01:15 AM
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
	abstract public class esHealthRecordLineCollection : esEntityCollectionWAuditLog
	{
		public esHealthRecordLineCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HealthRecordLineCollection";
		}

		#region Query Logic
		protected void InitQuery(esHealthRecordLineQuery query)
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
			this.InitQuery(query as esHealthRecordLineQuery);
		}
		#endregion
		
		virtual public HealthRecordLine DetachEntity(HealthRecordLine entity)
		{
			return base.DetachEntity(entity) as HealthRecordLine;
		}
		
		virtual public HealthRecordLine AttachEntity(HealthRecordLine entity)
		{
			return base.AttachEntity(entity) as HealthRecordLine;
		}
		
		virtual public void Combine(HealthRecordLineCollection collection)
		{
			base.Combine(collection);
		}
		
		new public HealthRecordLine this[int index]
		{
			get
			{
				return base[index] as HealthRecordLine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HealthRecordLine);
		}
	}



	[Serializable]
	abstract public class esHealthRecordLine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHealthRecordLineQuery GetDynamicQuery()
		{
			return null;
		}

		public esHealthRecordLine()
		{

		}

		public esHealthRecordLine(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String patientID, System.String questionFormID, System.String questionGroupID, System.String questionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, questionFormID, questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, questionFormID, questionGroupID, questionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientID, System.String questionFormID, System.String questionGroupID, System.String questionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, questionFormID, questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, questionFormID, questionGroupID, questionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String patientID, System.String questionFormID, System.String questionGroupID, System.String questionID)
		{
			esHealthRecordLineQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.QuestionFormID == questionFormID, query.QuestionGroupID == questionGroupID, query.QuestionID == questionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String patientID, System.String questionFormID, System.String questionGroupID, System.String questionID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);			parms.Add("QuestionFormID",questionFormID);			parms.Add("QuestionGroupID",questionGroupID);			parms.Add("QuestionID",questionID);
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
						case "PatientID": this.str.PatientID = (string)value; break;							
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
		/// Maps to HealthRecordLine.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionGroupID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionAnswerPrefix
		/// </summary>
		virtual public System.String QuestionAnswerPrefix
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerPrefix);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerPrefix, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionAnswerSuffix
		/// </summary>
		virtual public System.String QuestionAnswerSuffix
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerSuffix);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerSuffix, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionAnswerSelectionLineID
		/// </summary>
		virtual public System.String QuestionAnswerSelectionLineID
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerSelectionLineID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerSelectionLineID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionAnswerText
		/// </summary>
		virtual public System.String QuestionAnswerText
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerText);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.QuestionAnswerText, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.QuestionAnswerNum
		/// </summary>
		virtual public System.Decimal? QuestionAnswerNum
		{
			get
			{
				return base.GetSystemDecimal(HealthRecordLineMetadata.ColumnNames.QuestionAnswerNum);
			}
			
			set
			{
				base.SetSystemDecimal(HealthRecordLineMetadata.ColumnNames.QuestionAnswerNum, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HealthRecordLineMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(HealthRecordLineMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecordLine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HealthRecordLineMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordLineMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esHealthRecordLine entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
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
			

			private esHealthRecordLine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHealthRecordLineQuery query)
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
				throw new Exception("esHealthRecordLine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class HealthRecordLine : esHealthRecordLine
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
	abstract public class esHealthRecordLineQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HealthRecordLineMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionAnswerPrefix
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionAnswerPrefix, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionAnswerSuffix
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionAnswerSuffix, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionAnswerSelectionLineID
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionAnswerSelectionLineID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionAnswerText
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionAnswerText, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionAnswerNum
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.QuestionAnswerNum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HealthRecordLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HealthRecordLineCollection")]
	public partial class HealthRecordLineCollection : esHealthRecordLineCollection, IEnumerable<HealthRecordLine>
	{
		public HealthRecordLineCollection()
		{

		}
		
		public static implicit operator List<HealthRecordLine>(HealthRecordLineCollection coll)
		{
			List<HealthRecordLine> list = new List<HealthRecordLine>();
			
			foreach (HealthRecordLine emp in coll)
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
				return  HealthRecordLineMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HealthRecordLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HealthRecordLine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HealthRecordLine();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HealthRecordLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HealthRecordLineQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HealthRecordLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public HealthRecordLine AddNew()
		{
			HealthRecordLine entity = base.AddNewEntity() as HealthRecordLine;
			
			return entity;
		}

		public HealthRecordLine FindByPrimaryKey(System.String patientID, System.String questionFormID, System.String questionGroupID, System.String questionID)
		{
			return base.FindByPrimaryKey(patientID, questionFormID, questionGroupID, questionID) as HealthRecordLine;
		}


		#region IEnumerable<HealthRecordLine> Members

		IEnumerator<HealthRecordLine> IEnumerable<HealthRecordLine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as HealthRecordLine;
			}
		}

		#endregion
		
		private HealthRecordLineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HealthRecordLine' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("HealthRecordLine ({PatientID},{QuestionFormID},{QuestionGroupID},{QuestionID})")]
	[Serializable]
	public partial class HealthRecordLine : esHealthRecordLine
	{
		public HealthRecordLine()
		{

		}
	
		public HealthRecordLine(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HealthRecordLineMetadata.Meta();
			}
		}
		
		
		
		override protected esHealthRecordLineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HealthRecordLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HealthRecordLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HealthRecordLineQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HealthRecordLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HealthRecordLineQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HealthRecordLineQuery : esHealthRecordLineQuery
	{
		public HealthRecordLineQuery()
		{

		}		
		
		public HealthRecordLineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HealthRecordLineQuery";
        }
		
			
	}


	[Serializable]
	public partial class HealthRecordLineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HealthRecordLineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionGroupID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionAnswerPrefix, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionAnswerPrefix;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionAnswerSuffix, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionAnswerSuffix;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionAnswerSelectionLineID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionAnswerSelectionLineID;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionAnswerText, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionAnswerText;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.QuestionAnswerNum, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.QuestionAnswerNum;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordLineMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordLineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HealthRecordLineMetadata Meta()
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
			 public const string PatientID = "PatientID";
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
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientID = "PatientID";
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
			lock (typeof(HealthRecordLineMetadata))
			{
				if(HealthRecordLineMetadata.mapDelegates == null)
				{
					HealthRecordLineMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HealthRecordLineMetadata.meta == null)
				{
					HealthRecordLineMetadata.meta = new HealthRecordLineMetadata();
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
				

				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
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
				
				
				
				meta.Source = "HealthRecordLine";
				meta.Destination = "HealthRecordLine";
				
				meta.spInsert = "proc_HealthRecordLineInsert";				
				meta.spUpdate = "proc_HealthRecordLineUpdate";		
				meta.spDelete = "proc_HealthRecordLineDelete";
				meta.spLoadAll = "proc_HealthRecordLineLoadAll";
				meta.spLoadByPrimaryKey = "proc_HealthRecordLineLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HealthRecordLineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
