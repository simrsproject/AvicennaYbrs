/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esEmployeeLanguageProficiencyCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeLanguageProficiencyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeLanguageProficiencyCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeLanguageProficiencyQuery query)
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
			this.InitQuery(query as esEmployeeLanguageProficiencyQuery);
		}
		#endregion
		
		virtual public EmployeeLanguageProficiency DetachEntity(EmployeeLanguageProficiency entity)
		{
			return base.DetachEntity(entity) as EmployeeLanguageProficiency;
		}
		
		virtual public EmployeeLanguageProficiency AttachEntity(EmployeeLanguageProficiency entity)
		{
			return base.AttachEntity(entity) as EmployeeLanguageProficiency;
		}
		
		virtual public void Combine(EmployeeLanguageProficiencyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeLanguageProficiency this[int index]
		{
			get
			{
				return base[index] as EmployeeLanguageProficiency;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeLanguageProficiency);
		}
	}



	[Serializable]
	abstract public class esEmployeeLanguageProficiency : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeLanguageProficiencyQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeLanguageProficiency()
		{

		}

		public esEmployeeLanguageProficiency(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeLanguageProficiencyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLanguageProficiencyID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLanguageProficiencyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeLanguageProficiencyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLanguageProficiencyID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLanguageProficiencyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeLanguageProficiencyID)
		{
			esEmployeeLanguageProficiencyQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeLanguageProficiencyID == employeeLanguageProficiencyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeLanguageProficiencyID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeLanguageProficiencyID",employeeLanguageProficiencyID);
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
						case "EmployeeLanguageProficiencyID": this.str.EmployeeLanguageProficiencyID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "EvaluationDate": this.str.EvaluationDate = (string)value; break;							
						case "SRLanguage": this.str.SRLanguage = (string)value; break;							
						case "SRConversation": this.str.SRConversation = (string)value; break;							
						case "SRTranslation": this.str.SRTranslation = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeLanguageProficiencyID":
						
							if (value == null || value is System.Int32)
								this.EmployeeLanguageProficiencyID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "EvaluationDate":
						
							if (value == null || value is System.DateTime)
								this.EvaluationDate = (System.DateTime?)value;
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
		/// Maps to EmployeeLanguageProficiency.EmployeeLanguageProficiencyID
		/// </summary>
		virtual public System.Int32? EmployeeLanguageProficiencyID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLanguageProficiencyMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLanguageProficiencyMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.EvaluationDate
		/// </summary>
		virtual public System.DateTime? EvaluationDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLanguageProficiencyMetadata.ColumnNames.EvaluationDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLanguageProficiencyMetadata.ColumnNames.EvaluationDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.SRLanguage
		/// </summary>
		virtual public System.String SRLanguage
		{
			get
			{
				return base.GetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.SRLanguage);
			}
			
			set
			{
				base.SetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.SRLanguage, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.SRConversation
		/// </summary>
		virtual public System.String SRConversation
		{
			get
			{
				return base.GetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.SRConversation);
			}
			
			set
			{
				base.SetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.SRConversation, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.SRTranslation
		/// </summary>
		virtual public System.String SRTranslation
		{
			get
			{
				return base.GetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.SRTranslation);
			}
			
			set
			{
				base.SetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.SRTranslation, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLanguageProficiency.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeLanguageProficiency entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeLanguageProficiencyID
			{
				get
				{
					System.Int32? data = entity.EmployeeLanguageProficiencyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLanguageProficiencyID = null;
					else entity.EmployeeLanguageProficiencyID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String EvaluationDate
			{
				get
				{
					System.DateTime? data = entity.EvaluationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluationDate = null;
					else entity.EvaluationDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SRLanguage
			{
				get
				{
					System.String data = entity.SRLanguage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLanguage = null;
					else entity.SRLanguage = Convert.ToString(value);
				}
			}
				
			public System.String SRConversation
			{
				get
				{
					System.String data = entity.SRConversation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConversation = null;
					else entity.SRConversation = Convert.ToString(value);
				}
			}
				
			public System.String SRTranslation
			{
				get
				{
					System.String data = entity.SRTranslation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTranslation = null;
					else entity.SRTranslation = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esEmployeeLanguageProficiency entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeLanguageProficiencyQuery query)
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
				throw new Exception("esEmployeeLanguageProficiency can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeLanguageProficiency : esEmployeeLanguageProficiency
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
	abstract public class esEmployeeLanguageProficiencyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLanguageProficiencyMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeLanguageProficiencyID
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EvaluationDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.EvaluationDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRLanguage
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.SRLanguage, esSystemType.String);
			}
		} 
		
		public esQueryItem SRConversation
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.SRConversation, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTranslation
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.SRTranslation, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeLanguageProficiencyCollection")]
	public partial class EmployeeLanguageProficiencyCollection : esEmployeeLanguageProficiencyCollection, IEnumerable<EmployeeLanguageProficiency>
	{
		public EmployeeLanguageProficiencyCollection()
		{

		}
		
		public static implicit operator List<EmployeeLanguageProficiency>(EmployeeLanguageProficiencyCollection coll)
		{
			List<EmployeeLanguageProficiency> list = new List<EmployeeLanguageProficiency>();
			
			foreach (EmployeeLanguageProficiency emp in coll)
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
				return  EmployeeLanguageProficiencyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLanguageProficiencyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeLanguageProficiency(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeLanguageProficiency();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeLanguageProficiencyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLanguageProficiencyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeLanguageProficiencyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeLanguageProficiency AddNew()
		{
			EmployeeLanguageProficiency entity = base.AddNewEntity() as EmployeeLanguageProficiency;
			
			return entity;
		}

		public EmployeeLanguageProficiency FindByPrimaryKey(System.Int32 employeeLanguageProficiencyID)
		{
			return base.FindByPrimaryKey(employeeLanguageProficiencyID) as EmployeeLanguageProficiency;
		}


		#region IEnumerable<EmployeeLanguageProficiency> Members

		IEnumerator<EmployeeLanguageProficiency> IEnumerable<EmployeeLanguageProficiency>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeLanguageProficiency;
			}
		}

		#endregion
		
		private EmployeeLanguageProficiencyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeLanguageProficiency' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeLanguageProficiency ({EmployeeLanguageProficiencyID})")]
	[Serializable]
	public partial class EmployeeLanguageProficiency : esEmployeeLanguageProficiency
	{
		public EmployeeLanguageProficiency()
		{

		}
	
		public EmployeeLanguageProficiency(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLanguageProficiencyMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeLanguageProficiencyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLanguageProficiencyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeLanguageProficiencyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLanguageProficiencyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeLanguageProficiencyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeLanguageProficiencyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeLanguageProficiencyQuery : esEmployeeLanguageProficiencyQuery
	{
		public EmployeeLanguageProficiencyQuery()
		{

		}		
		
		public EmployeeLanguageProficiencyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeLanguageProficiencyQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeLanguageProficiencyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeLanguageProficiencyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.EmployeeLanguageProficiencyID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.EvaluationDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.EvaluationDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.SRLanguage, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.SRLanguage;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.SRConversation, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.SRConversation;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.SRTranslation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.SRTranslation;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLanguageProficiencyMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLanguageProficiencyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeLanguageProficiencyMetadata Meta()
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
			 public const string EmployeeLanguageProficiencyID = "EmployeeLanguageProficiencyID";
			 public const string PersonID = "PersonID";
			 public const string EvaluationDate = "EvaluationDate";
			 public const string SRLanguage = "SRLanguage";
			 public const string SRConversation = "SRConversation";
			 public const string SRTranslation = "SRTranslation";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeLanguageProficiencyID = "EmployeeLanguageProficiencyID";
			 public const string PersonID = "PersonID";
			 public const string EvaluationDate = "EvaluationDate";
			 public const string SRLanguage = "SRLanguage";
			 public const string SRConversation = "SRConversation";
			 public const string SRTranslation = "SRTranslation";
			 public const string Notes = "Notes";
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
			lock (typeof(EmployeeLanguageProficiencyMetadata))
			{
				if(EmployeeLanguageProficiencyMetadata.mapDelegates == null)
				{
					EmployeeLanguageProficiencyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeLanguageProficiencyMetadata.meta == null)
				{
					EmployeeLanguageProficiencyMetadata.meta = new EmployeeLanguageProficiencyMetadata();
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
				

				meta.AddTypeMap("EmployeeLanguageProficiencyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EvaluationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRLanguage", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConversation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTranslation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeLanguageProficiency";
				meta.Destination = "EmployeeLanguageProficiency";
				
				meta.spInsert = "proc_EmployeeLanguageProficiencyInsert";				
				meta.spUpdate = "proc_EmployeeLanguageProficiencyUpdate";		
				meta.spDelete = "proc_EmployeeLanguageProficiencyDelete";
				meta.spLoadAll = "proc_EmployeeLanguageProficiencyLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLanguageProficiencyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeLanguageProficiencyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
