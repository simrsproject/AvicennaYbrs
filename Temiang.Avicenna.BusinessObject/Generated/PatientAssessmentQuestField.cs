/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/21/2023 12:29:40 PM
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
	abstract public class esPatientAssessmentQuestFieldCollection : esEntityCollectionWAuditLog
	{
		public esPatientAssessmentQuestFieldCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientAssessmentQuestFieldCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientAssessmentQuestFieldQuery query)
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
			this.InitQuery(query as esPatientAssessmentQuestFieldQuery);
		}
		#endregion
			
		virtual public PatientAssessmentQuestField DetachEntity(PatientAssessmentQuestField entity)
		{
			return base.DetachEntity(entity) as PatientAssessmentQuestField;
		}
		
		virtual public PatientAssessmentQuestField AttachEntity(PatientAssessmentQuestField entity)
		{
			return base.AttachEntity(entity) as PatientAssessmentQuestField;
		}
		
		virtual public void Combine(PatientAssessmentQuestFieldCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientAssessmentQuestField this[int index]
		{
			get
			{
				return base[index] as PatientAssessmentQuestField;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientAssessmentQuestField);
		}
	}

	[Serializable]
	abstract public class esPatientAssessmentQuestField : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientAssessmentQuestFieldQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientAssessmentQuestField()
		{
		}
	
		public esPatientAssessmentQuestField(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationInfoMedicID, String questionGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, questionGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, questionGroupID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationInfoMedicID, String questionGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, questionGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, questionGroupID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationInfoMedicID, String questionGroupID)
		{
			esPatientAssessmentQuestFieldQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID, query.QuestionGroupID == questionGroupID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationInfoMedicID, String questionGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationInfoMedicID",registrationInfoMedicID);
			parms.Add("QuestionGroupID",questionGroupID);
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
						case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
						case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;
						case "QuestionAnswer": this.str.QuestionAnswer = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
					
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
		/// Maps to PatientAssessmentQuestField.RegistrationInfoMedicID
		/// </summary>
		virtual public System.String RegistrationInfoMedicID
		{
			get
			{
				return base.GetSystemString(PatientAssessmentQuestFieldMetadata.ColumnNames.RegistrationInfoMedicID);
			}
			
			set
			{
				base.SetSystemString(PatientAssessmentQuestFieldMetadata.ColumnNames.RegistrationInfoMedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentQuestField.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionGroupID);
			}
			
			set
			{
				base.SetSystemString(PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentQuestField.QuestionAnswer
		/// </summary>
		virtual public System.String QuestionAnswer
		{
			get
			{
				return base.GetSystemString(PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionAnswer);
			}
			
			set
			{
				base.SetSystemString(PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionAnswer, value);
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
			public esStrings(esPatientAssessmentQuestField entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationInfoMedicID
			{
				get
				{
					System.String data = entity.RegistrationInfoMedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
					else entity.RegistrationInfoMedicID = Convert.ToString(value);
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
			public System.String QuestionAnswer
			{
				get
				{
					System.String data = entity.QuestionAnswer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswer = null;
					else entity.QuestionAnswer = Convert.ToString(value);
				}
			}
			private esPatientAssessmentQuestField entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientAssessmentQuestFieldQuery query)
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
				throw new Exception("esPatientAssessmentQuestField can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientAssessmentQuestField : esPatientAssessmentQuestField
	{	
	}

	[Serializable]
	abstract public class esPatientAssessmentQuestFieldQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientAssessmentQuestFieldMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationInfoMedicID
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentQuestFieldMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswer
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionAnswer, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientAssessmentQuestFieldCollection")]
	public partial class PatientAssessmentQuestFieldCollection : esPatientAssessmentQuestFieldCollection, IEnumerable< PatientAssessmentQuestField>
	{
		public PatientAssessmentQuestFieldCollection()
		{

		}	
		
		public static implicit operator List< PatientAssessmentQuestField>(PatientAssessmentQuestFieldCollection coll)
		{
			List< PatientAssessmentQuestField> list = new List< PatientAssessmentQuestField>();
			
			foreach (PatientAssessmentQuestField emp in coll)
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
				return  PatientAssessmentQuestFieldMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientAssessmentQuestFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientAssessmentQuestField(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientAssessmentQuestField();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientAssessmentQuestFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientAssessmentQuestFieldQuery();
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
		public bool Load(PatientAssessmentQuestFieldQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientAssessmentQuestField AddNew()
		{
			PatientAssessmentQuestField entity = base.AddNewEntity() as PatientAssessmentQuestField;
			
			return entity;		
		}
		public PatientAssessmentQuestField FindByPrimaryKey(String registrationInfoMedicID, String questionGroupID)
		{
			return base.FindByPrimaryKey(registrationInfoMedicID, questionGroupID) as PatientAssessmentQuestField;
		}

		#region IEnumerable< PatientAssessmentQuestField> Members

		IEnumerator< PatientAssessmentQuestField> IEnumerable< PatientAssessmentQuestField>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientAssessmentQuestField;
			}
		}

		#endregion
		
		private PatientAssessmentQuestFieldQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientAssessmentQuestField' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientAssessmentQuestField ({RegistrationInfoMedicID, QuestionGroupID})")]
	[Serializable]
	public partial class PatientAssessmentQuestField : esPatientAssessmentQuestField
	{
		public PatientAssessmentQuestField()
		{
		}	
	
		public PatientAssessmentQuestField(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientAssessmentQuestFieldMetadata.Meta();
			}
		}	
	
		override protected esPatientAssessmentQuestFieldQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientAssessmentQuestFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientAssessmentQuestFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientAssessmentQuestFieldQuery();
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
		public bool Load(PatientAssessmentQuestFieldQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientAssessmentQuestFieldQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientAssessmentQuestFieldQuery : esPatientAssessmentQuestFieldQuery
	{
		public PatientAssessmentQuestFieldQuery()
		{

		}		
		
		public PatientAssessmentQuestFieldQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientAssessmentQuestFieldQuery";
        }
	}

	[Serializable]
	public partial class PatientAssessmentQuestFieldMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientAssessmentQuestFieldMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientAssessmentQuestFieldMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientAssessmentQuestFieldMetadata.PropertyNames.RegistrationInfoMedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionGroupID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientAssessmentQuestFieldMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentQuestFieldMetadata.ColumnNames.QuestionAnswer, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientAssessmentQuestFieldMetadata.PropertyNames.QuestionAnswer;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientAssessmentQuestFieldMetadata Meta()
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
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionAnswer = "QuestionAnswer";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionAnswer = "QuestionAnswer";
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
			lock (typeof(PatientAssessmentQuestFieldMetadata))
			{
				if(PatientAssessmentQuestFieldMetadata.mapDelegates == null)
				{
					PatientAssessmentQuestFieldMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientAssessmentQuestFieldMetadata.meta == null)
				{
					PatientAssessmentQuestFieldMetadata.meta = new PatientAssessmentQuestFieldMetadata();
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
				
				meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswer", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PatientAssessmentQuestField";
				meta.Destination = "PatientAssessmentQuestField";
				meta.spInsert = "proc_PatientAssessmentQuestFieldInsert";				
				meta.spUpdate = "proc_PatientAssessmentQuestFieldUpdate";		
				meta.spDelete = "proc_PatientAssessmentQuestFieldDelete";
				meta.spLoadAll = "proc_PatientAssessmentQuestFieldLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientAssessmentQuestFieldLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientAssessmentQuestFieldMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
