/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/21/2014 11:39:23 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Casemix
{

	[Serializable]
	abstract public class esDiagnosisCollection : esEntityCollectionWAuditLog
	{
		public esDiagnosisCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DiagnosisCollection";
		}

		#region Query Logic
		protected void InitQuery(esDiagnosisQuery query)
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
			this.InitQuery(query as esDiagnosisQuery);
		}
		#endregion
		
		virtual public Diagnosis DetachEntity(Diagnosis entity)
		{
			return base.DetachEntity(entity) as Diagnosis;
		}
		
		virtual public Diagnosis AttachEntity(Diagnosis entity)
		{
			return base.AttachEntity(entity) as Diagnosis;
		}
		
		virtual public void Combine(DiagnosisCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Diagnosis this[int index]
		{
			get
			{
				return base[index] as Diagnosis;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Diagnosis);
		}
	}



	[Serializable]
	abstract public class esDiagnosis : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDiagnosisQuery GetDynamicQuery()
		{
			return null;
		}

		public esDiagnosis()
		{

		}

		public esDiagnosis(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String code)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(code);
			else
				return LoadByPrimaryKeyStoredProcedure(code);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String code)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(code);
			else
				return LoadByPrimaryKeyStoredProcedure(code);
		}

		private bool LoadByPrimaryKeyDynamic(System.String code)
		{
			esDiagnosisQuery query = this.GetDynamicQuery();
			query.Where(query.Code == code);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String code)
		{
			esParameters parms = new esParameters();
			parms.Add("Code",code);
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
						case "Code": this.str.Code = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "Severity": this.str.Severity = (string)value; break;							
						case "Inpatient": this.str.Inpatient = (string)value; break;							
						case "OutPatient": this.str.OutPatient = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Severity":
						
							if (value == null || value is System.Int32)
								this.Severity = (System.Int32?)value;
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
		/// Maps to diagnosis.Code
		/// </summary>
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(DiagnosisMetadata.ColumnNames.Code);
			}
			
			set
			{
				base.SetSystemString(DiagnosisMetadata.ColumnNames.Code, value);
			}
		}
		
		/// <summary>
		/// Maps to diagnosis.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(DiagnosisMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(DiagnosisMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to diagnosis.Severity
		/// </summary>
		virtual public System.Int32? Severity
		{
			get
			{
				return base.GetSystemInt32(DiagnosisMetadata.ColumnNames.Severity);
			}
			
			set
			{
				base.SetSystemInt32(DiagnosisMetadata.ColumnNames.Severity, value);
			}
		}
		
		/// <summary>
		/// Maps to diagnosis.Inpatient
		/// </summary>
		virtual public System.String Inpatient
		{
			get
			{
				return base.GetSystemString(DiagnosisMetadata.ColumnNames.Inpatient);
			}
			
			set
			{
				base.SetSystemString(DiagnosisMetadata.ColumnNames.Inpatient, value);
			}
		}
		
		/// <summary>
		/// Maps to diagnosis.OutPatient
		/// </summary>
		virtual public System.String OutPatient
		{
			get
			{
				return base.GetSystemString(DiagnosisMetadata.ColumnNames.OutPatient);
			}
			
			set
			{
				base.SetSystemString(DiagnosisMetadata.ColumnNames.OutPatient, value);
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
			public esStrings(esDiagnosis entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Code
			{
				get
				{
					System.String data = entity.Code;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Code = null;
					else entity.Code = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String Severity
			{
				get
				{
					System.Int32? data = entity.Severity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Severity = null;
					else entity.Severity = Convert.ToInt32(value);
				}
			}
				
			public System.String Inpatient
			{
				get
				{
					System.String data = entity.Inpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Inpatient = null;
					else entity.Inpatient = Convert.ToString(value);
				}
			}
				
			public System.String OutPatient
			{
				get
				{
					System.String data = entity.OutPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OutPatient = null;
					else entity.OutPatient = Convert.ToString(value);
				}
			}
			

			private esDiagnosis entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDiagnosisQuery query)
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
				throw new Exception("esDiagnosis can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esDiagnosisQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DiagnosisMetadata.Meta();
			}
		}	
		

		public esQueryItem Code
		{
			get
			{
				return new esQueryItem(this, DiagnosisMetadata.ColumnNames.Code, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, DiagnosisMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem Severity
		{
			get
			{
				return new esQueryItem(this, DiagnosisMetadata.ColumnNames.Severity, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Inpatient
		{
			get
			{
				return new esQueryItem(this, DiagnosisMetadata.ColumnNames.Inpatient, esSystemType.String);
			}
		} 
		
		public esQueryItem OutPatient
		{
			get
			{
				return new esQueryItem(this, DiagnosisMetadata.ColumnNames.OutPatient, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DiagnosisCollection")]
	public partial class DiagnosisCollection : esDiagnosisCollection, IEnumerable<Diagnosis>
	{
		public DiagnosisCollection()
		{

		}
		
		public static implicit operator List<Diagnosis>(DiagnosisCollection coll)
		{
			List<Diagnosis> list = new List<Diagnosis>();
			
			foreach (Diagnosis emp in coll)
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
				return  DiagnosisMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DiagnosisQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Diagnosis(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Diagnosis();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DiagnosisQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DiagnosisQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DiagnosisQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Diagnosis AddNew()
		{
			Diagnosis entity = base.AddNewEntity() as Diagnosis;
			
			return entity;
		}

		public Diagnosis FindByPrimaryKey(System.String code)
		{
			return base.FindByPrimaryKey(code) as Diagnosis;
		}


		#region IEnumerable<Diagnosis> Members

		IEnumerator<Diagnosis> IEnumerable<Diagnosis>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Diagnosis;
			}
		}

		#endregion
		
		private DiagnosisQuery query;
	}


	/// <summary>
	/// Encapsulates the 'diagnosis' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Diagnosis ({Code})")]
	[Serializable]
	public partial class Diagnosis : esDiagnosis
	{
		public Diagnosis()
		{

		}
	
		public Diagnosis(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DiagnosisMetadata.Meta();
			}
		}
		
		
		
		override protected esDiagnosisQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DiagnosisQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DiagnosisQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DiagnosisQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DiagnosisQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DiagnosisQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DiagnosisQuery : esDiagnosisQuery
	{
		public DiagnosisQuery()
		{

		}		
		
		public DiagnosisQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DiagnosisQuery";
        }
		
			
	}


	[Serializable]
	public partial class DiagnosisMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DiagnosisMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DiagnosisMetadata.ColumnNames.Code, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnosisMetadata.PropertyNames.Code;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnosisMetadata.ColumnNames.Description, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnosisMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 159;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnosisMetadata.ColumnNames.Severity, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DiagnosisMetadata.PropertyNames.Severity;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnosisMetadata.ColumnNames.Inpatient, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnosisMetadata.PropertyNames.Inpatient;
			c.CharacterMaxLength = 39;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnosisMetadata.ColumnNames.OutPatient, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnosisMetadata.PropertyNames.OutPatient;
			c.CharacterMaxLength = 103;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DiagnosisMetadata Meta()
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
			 public const string Code = "Code";
			 public const string Description = "Description";
			 public const string Severity = "Severity";
			 public const string Inpatient = "Inpatient";
			 public const string OutPatient = "OutPatient";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Code = "Code";
			 public const string Description = "Description";
			 public const string Severity = "Severity";
			 public const string Inpatient = "Inpatient";
			 public const string OutPatient = "OutPatient";
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
			lock (typeof(DiagnosisMetadata))
			{
				if(DiagnosisMetadata.mapDelegates == null)
				{
					DiagnosisMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DiagnosisMetadata.meta == null)
				{
					DiagnosisMetadata.meta = new DiagnosisMetadata();
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
				

				meta.AddTypeMap("Code", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Severity", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Inpatient", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OutPatient", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "diagnosis";
				meta.Destination = "diagnosis";
				
				meta.spInsert = "proc_diagnosisInsert";				
				meta.spUpdate = "proc_diagnosisUpdate";		
				meta.spDelete = "proc_diagnosisDelete";
				meta.spLoadAll = "proc_diagnosisLoadAll";
				meta.spLoadByPrimaryKey = "proc_diagnosisLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DiagnosisMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
