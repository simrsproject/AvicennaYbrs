/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/2/2015 11:28:43 AM
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
	abstract public class esDhfPatientLaboratoryResultsCollection : esEntityCollectionWAuditLog
	{
		public esDhfPatientLaboratoryResultsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DhfPatientLaboratoryResultsCollection";
		}

		#region Query Logic
		protected void InitQuery(esDhfPatientLaboratoryResultsQuery query)
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
			this.InitQuery(query as esDhfPatientLaboratoryResultsQuery);
		}
		#endregion
		
		virtual public DhfPatientLaboratoryResults DetachEntity(DhfPatientLaboratoryResults entity)
		{
			return base.DetachEntity(entity) as DhfPatientLaboratoryResults;
		}
		
		virtual public DhfPatientLaboratoryResults AttachEntity(DhfPatientLaboratoryResults entity)
		{
			return base.AttachEntity(entity) as DhfPatientLaboratoryResults;
		}
		
		virtual public void Combine(DhfPatientLaboratoryResultsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DhfPatientLaboratoryResults this[int index]
		{
			get
			{
				return base[index] as DhfPatientLaboratoryResults;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DhfPatientLaboratoryResults);
		}
	}



	[Serializable]
	abstract public class esDhfPatientLaboratoryResults : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDhfPatientLaboratoryResultsQuery GetDynamicQuery()
		{
			return null;
		}

		public esDhfPatientLaboratoryResults()
		{

		}

		public esDhfPatientLaboratoryResults(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
		{
			esDhfPatientLaboratoryResultsQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "Days": this.str.Days = (string)value; break;							
						case "Leukosit": this.str.Leukosit = (string)value; break;							
						case "Trombosit": this.str.Trombosit = (string)value; break;							
						case "Hematokrit": this.str.Hematokrit = (string)value; break;							
						case "Hemoglobin": this.str.Hemoglobin = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Days":
						
							if (value == null || value is System.Int16)
								this.Days = (System.Int16?)value;
							break;
						
						case "Leukosit":
						
							if (value == null || value is System.Decimal)
								this.Leukosit = (System.Decimal?)value;
							break;
						
						case "Trombosit":
						
							if (value == null || value is System.Decimal)
								this.Trombosit = (System.Decimal?)value;
							break;
						
						case "Hematokrit":
						
							if (value == null || value is System.Decimal)
								this.Hematokrit = (System.Decimal?)value;
							break;
						
						case "Hemoglobin":
						
							if (value == null || value is System.Decimal)
								this.Hemoglobin = (System.Decimal?)value;
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
		/// Maps to DhfPatientLaboratoryResults.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(DhfPatientLaboratoryResultsMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(DhfPatientLaboratoryResultsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.Days
		/// </summary>
		virtual public System.Int16? Days
		{
			get
			{
				return base.GetSystemInt16(DhfPatientLaboratoryResultsMetadata.ColumnNames.Days);
			}
			
			set
			{
				base.SetSystemInt16(DhfPatientLaboratoryResultsMetadata.ColumnNames.Days, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.Leukosit
		/// </summary>
		virtual public System.Decimal? Leukosit
		{
			get
			{
				return base.GetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Leukosit);
			}
			
			set
			{
				base.SetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Leukosit, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.Trombosit
		/// </summary>
		virtual public System.Decimal? Trombosit
		{
			get
			{
				return base.GetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Trombosit);
			}
			
			set
			{
				base.SetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Trombosit, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.Hematokrit
		/// </summary>
		virtual public System.Decimal? Hematokrit
		{
			get
			{
				return base.GetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Hematokrit);
			}
			
			set
			{
				base.SetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Hematokrit, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.Hemoglobin
		/// </summary>
		virtual public System.Decimal? Hemoglobin
		{
			get
			{
				return base.GetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Hemoglobin);
			}
			
			set
			{
				base.SetSystemDecimal(DhfPatientLaboratoryResultsMetadata.ColumnNames.Hemoglobin, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DhfPatientLaboratoryResults.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDhfPatientLaboratoryResults entity)
			{
				this.entity = entity;
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
				
			public System.String Days
			{
				get
				{
					System.Int16? data = entity.Days;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Days = null;
					else entity.Days = Convert.ToInt16(value);
				}
			}
				
			public System.String Leukosit
			{
				get
				{
					System.Decimal? data = entity.Leukosit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Leukosit = null;
					else entity.Leukosit = Convert.ToDecimal(value);
				}
			}
				
			public System.String Trombosit
			{
				get
				{
					System.Decimal? data = entity.Trombosit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Trombosit = null;
					else entity.Trombosit = Convert.ToDecimal(value);
				}
			}
				
			public System.String Hematokrit
			{
				get
				{
					System.Decimal? data = entity.Hematokrit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematokrit = null;
					else entity.Hematokrit = Convert.ToDecimal(value);
				}
			}
				
			public System.String Hemoglobin
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin = null;
					else entity.Hemoglobin = Convert.ToDecimal(value);
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
			

			private esDhfPatientLaboratoryResults entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDhfPatientLaboratoryResultsQuery query)
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
				throw new Exception("esDhfPatientLaboratoryResults can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class DhfPatientLaboratoryResults : esDhfPatientLaboratoryResults
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
	abstract public class esDhfPatientLaboratoryResultsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DhfPatientLaboratoryResultsMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Days
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.Days, esSystemType.Int16);
			}
		} 
		
		public esQueryItem Leukosit
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.Leukosit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Trombosit
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.Trombosit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Hematokrit
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.Hematokrit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Hemoglobin
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.Hemoglobin, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DhfPatientLaboratoryResultsCollection")]
	public partial class DhfPatientLaboratoryResultsCollection : esDhfPatientLaboratoryResultsCollection, IEnumerable<DhfPatientLaboratoryResults>
	{
		public DhfPatientLaboratoryResultsCollection()
		{

		}
		
		public static implicit operator List<DhfPatientLaboratoryResults>(DhfPatientLaboratoryResultsCollection coll)
		{
			List<DhfPatientLaboratoryResults> list = new List<DhfPatientLaboratoryResults>();
			
			foreach (DhfPatientLaboratoryResults emp in coll)
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
				return  DhfPatientLaboratoryResultsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DhfPatientLaboratoryResultsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DhfPatientLaboratoryResults(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DhfPatientLaboratoryResults();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DhfPatientLaboratoryResultsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DhfPatientLaboratoryResultsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DhfPatientLaboratoryResultsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DhfPatientLaboratoryResults AddNew()
		{
			DhfPatientLaboratoryResults entity = base.AddNewEntity() as DhfPatientLaboratoryResults;
			
			return entity;
		}

		public DhfPatientLaboratoryResults FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as DhfPatientLaboratoryResults;
		}


		#region IEnumerable<DhfPatientLaboratoryResults> Members

		IEnumerator<DhfPatientLaboratoryResults> IEnumerable<DhfPatientLaboratoryResults>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DhfPatientLaboratoryResults;
			}
		}

		#endregion
		
		private DhfPatientLaboratoryResultsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DhfPatientLaboratoryResults' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DhfPatientLaboratoryResults ({RegistrationNo})")]
	[Serializable]
	public partial class DhfPatientLaboratoryResults : esDhfPatientLaboratoryResults
	{
		public DhfPatientLaboratoryResults()
		{

		}
	
		public DhfPatientLaboratoryResults(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DhfPatientLaboratoryResultsMetadata.Meta();
			}
		}
		
		
		
		override protected esDhfPatientLaboratoryResultsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DhfPatientLaboratoryResultsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DhfPatientLaboratoryResultsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DhfPatientLaboratoryResultsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DhfPatientLaboratoryResultsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DhfPatientLaboratoryResultsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DhfPatientLaboratoryResultsQuery : esDhfPatientLaboratoryResultsQuery
	{
		public DhfPatientLaboratoryResultsQuery()
		{

		}		
		
		public DhfPatientLaboratoryResultsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DhfPatientLaboratoryResultsQuery";
        }
		
			
	}


	[Serializable]
	public partial class DhfPatientLaboratoryResultsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DhfPatientLaboratoryResultsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.Days, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.Days;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.Leukosit, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.Leukosit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.Trombosit, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.Trombosit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.Hematokrit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.Hematokrit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.Hemoglobin, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.Hemoglobin;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DhfPatientLaboratoryResultsMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DhfPatientLaboratoryResultsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DhfPatientLaboratoryResultsMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string Days = "Days";
			 public const string Leukosit = "Leukosit";
			 public const string Trombosit = "Trombosit";
			 public const string Hematokrit = "Hematokrit";
			 public const string Hemoglobin = "Hemoglobin";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string Days = "Days";
			 public const string Leukosit = "Leukosit";
			 public const string Trombosit = "Trombosit";
			 public const string Hematokrit = "Hematokrit";
			 public const string Hemoglobin = "Hemoglobin";
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
			lock (typeof(DhfPatientLaboratoryResultsMetadata))
			{
				if(DhfPatientLaboratoryResultsMetadata.mapDelegates == null)
				{
					DhfPatientLaboratoryResultsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DhfPatientLaboratoryResultsMetadata.meta == null)
				{
					DhfPatientLaboratoryResultsMetadata.meta = new DhfPatientLaboratoryResultsMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Days", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Leukosit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Trombosit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematokrit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DhfPatientLaboratoryResults";
				meta.Destination = "DhfPatientLaboratoryResults";
				
				meta.spInsert = "proc_DhfPatientLaboratoryResultsInsert";				
				meta.spUpdate = "proc_DhfPatientLaboratoryResultsUpdate";		
				meta.spDelete = "proc_DhfPatientLaboratoryResultsDelete";
				meta.spLoadAll = "proc_DhfPatientLaboratoryResultsLoadAll";
				meta.spLoadByPrimaryKey = "proc_DhfPatientLaboratoryResultsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DhfPatientLaboratoryResultsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
