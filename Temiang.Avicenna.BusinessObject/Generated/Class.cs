/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2018 5:11:38 PM
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
	abstract public class esClassCollection : esEntityCollectionWAuditLog
	{
		public esClassCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ClassCollection";
		}

		#region Query Logic
		protected void InitQuery(esClassQuery query)
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
			this.InitQuery(query as esClassQuery);
		}
		#endregion
		
		virtual public Class DetachEntity(Class entity)
		{
			return base.DetachEntity(entity) as Class;
		}
		
		virtual public Class AttachEntity(Class entity)
		{
			return base.AttachEntity(entity) as Class;
		}
		
		virtual public void Combine(ClassCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Class this[int index]
		{
			get
			{
				return base[index] as Class;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Class);
		}
	}



	[Serializable]
	abstract public class esClass : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClassQuery GetDynamicQuery()
		{
			return null;
		}

		public esClass()
		{

		}

		public esClass(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String classID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(classID);
			else
				return LoadByPrimaryKeyStoredProcedure(classID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String classID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(classID);
			else
				return LoadByPrimaryKeyStoredProcedure(classID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String classID)
		{
			esClassQuery query = this.GetDynamicQuery();
			query.Where(query.ClassID == classID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String classID)
		{
			esParameters parms = new esParameters();
			parms.Add("ClassID",classID);
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
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "ClassName": this.str.ClassName = (string)value; break;							
						case "ShortName": this.str.ShortName = (string)value; break;							
						case "SRClassRL": this.str.SRClassRL = (string)value; break;							
						case "MarginPercentage": this.str.MarginPercentage = (string)value; break;							
						case "DepositAmount": this.str.DepositAmount = (string)value; break;							
						case "IsInPatientClass": this.str.IsInPatientClass = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "Margin2Percentage": this.str.Margin2Percentage = (string)value; break;							
						case "BpjsClassID": this.str.BpjsClassID = (string)value; break;							
						case "IsTariffClass": this.str.IsTariffClass = (string)value; break;							
						case "ClassSeq": this.str.ClassSeq = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MarginPercentage":
						
							if (value == null || value is System.Decimal)
								this.MarginPercentage = (System.Decimal?)value;
							break;
						
						case "DepositAmount":
						
							if (value == null || value is System.Decimal)
								this.DepositAmount = (System.Decimal?)value;
							break;
						
						case "IsInPatientClass":
						
							if (value == null || value is System.Boolean)
								this.IsInPatientClass = (System.Boolean?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "Margin2Percentage":
						
							if (value == null || value is System.Decimal)
								this.Margin2Percentage = (System.Decimal?)value;
							break;
						
						case "IsTariffClass":
						
							if (value == null || value is System.Boolean)
								this.IsTariffClass = (System.Boolean?)value;
							break;
						
						case "ClassSeq":
						
							if (value == null || value is System.Int16)
								this.ClassSeq = (System.Int16?)value;
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
		/// Maps to Class.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ClassMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ClassMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.ClassName
		/// </summary>
		virtual public System.String ClassName
		{
			get
			{
				return base.GetSystemString(ClassMetadata.ColumnNames.ClassName);
			}
			
			set
			{
				base.SetSystemString(ClassMetadata.ColumnNames.ClassName, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(ClassMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(ClassMetadata.ColumnNames.ShortName, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.SRClassRL
		/// </summary>
		virtual public System.String SRClassRL
		{
			get
			{
				return base.GetSystemString(ClassMetadata.ColumnNames.SRClassRL);
			}
			
			set
			{
				base.SetSystemString(ClassMetadata.ColumnNames.SRClassRL, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.MarginPercentage
		/// </summary>
		virtual public System.Decimal? MarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ClassMetadata.ColumnNames.MarginPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(ClassMetadata.ColumnNames.MarginPercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.DepositAmount
		/// </summary>
		virtual public System.Decimal? DepositAmount
		{
			get
			{
				return base.GetSystemDecimal(ClassMetadata.ColumnNames.DepositAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ClassMetadata.ColumnNames.DepositAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.IsInPatientClass
		/// </summary>
		virtual public System.Boolean? IsInPatientClass
		{
			get
			{
				return base.GetSystemBoolean(ClassMetadata.ColumnNames.IsInPatientClass);
			}
			
			set
			{
				base.SetSystemBoolean(ClassMetadata.ColumnNames.IsInPatientClass, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ClassMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ClassMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClassMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ClassMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClassMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ClassMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.Margin2Percentage
		/// </summary>
		virtual public System.Decimal? Margin2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ClassMetadata.ColumnNames.Margin2Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ClassMetadata.ColumnNames.Margin2Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.BpjsClassID
		/// </summary>
		virtual public System.String BpjsClassID
		{
			get
			{
				return base.GetSystemString(ClassMetadata.ColumnNames.BpjsClassID);
			}
			
			set
			{
				base.SetSystemString(ClassMetadata.ColumnNames.BpjsClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.IsTariffClass
		/// </summary>
		virtual public System.Boolean? IsTariffClass
		{
			get
			{
				return base.GetSystemBoolean(ClassMetadata.ColumnNames.IsTariffClass);
			}
			
			set
			{
				base.SetSystemBoolean(ClassMetadata.ColumnNames.IsTariffClass, value);
			}
		}
		
		/// <summary>
		/// Maps to Class.ClassSeq
		/// </summary>
		virtual public System.Int16? ClassSeq
		{
			get
			{
				return base.GetSystemInt16(ClassMetadata.ColumnNames.ClassSeq);
			}
			
			set
			{
				base.SetSystemInt16(ClassMetadata.ColumnNames.ClassSeq, value);
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
			public esStrings(esClass entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String ClassName
			{
				get
				{
					System.String data = entity.ClassName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassName = null;
					else entity.ClassName = Convert.ToString(value);
				}
			}
				
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
				
			public System.String SRClassRL
			{
				get
				{
					System.String data = entity.SRClassRL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClassRL = null;
					else entity.SRClassRL = Convert.ToString(value);
				}
			}
				
			public System.String MarginPercentage
			{
				get
				{
					System.Decimal? data = entity.MarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginPercentage = null;
					else entity.MarginPercentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String DepositAmount
			{
				get
				{
					System.Decimal? data = entity.DepositAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepositAmount = null;
					else entity.DepositAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsInPatientClass
			{
				get
				{
					System.Boolean? data = entity.IsInPatientClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInPatientClass = null;
					else entity.IsInPatientClass = Convert.ToBoolean(value);
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
				
			public System.String Margin2Percentage
			{
				get
				{
					System.Decimal? data = entity.Margin2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Margin2Percentage = null;
					else entity.Margin2Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String BpjsClassID
			{
				get
				{
					System.String data = entity.BpjsClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsClassID = null;
					else entity.BpjsClassID = Convert.ToString(value);
				}
			}
				
			public System.String IsTariffClass
			{
				get
				{
					System.Boolean? data = entity.IsTariffClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTariffClass = null;
					else entity.IsTariffClass = Convert.ToBoolean(value);
				}
			}
				
			public System.String ClassSeq
			{
				get
				{
					System.Int16? data = entity.ClassSeq;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassSeq = null;
					else entity.ClassSeq = Convert.ToInt16(value);
				}
			}
			

			private esClass entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClassQuery query)
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
				throw new Exception("esClass can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Class : esClass
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
	abstract public class esClassQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ClassMetadata.Meta();
			}
		}	
		

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassName
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.ClassName, esSystemType.String);
			}
		} 
		
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRClassRL
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.SRClassRL, esSystemType.String);
			}
		} 
		
		public esQueryItem MarginPercentage
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.MarginPercentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DepositAmount
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.DepositAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsInPatientClass
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.IsInPatientClass, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem Margin2Percentage
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.Margin2Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem BpjsClassID
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.BpjsClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsTariffClass
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.IsTariffClass, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ClassSeq
		{
			get
			{
				return new esQueryItem(this, ClassMetadata.ColumnNames.ClassSeq, esSystemType.Int16);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClassCollection")]
	public partial class ClassCollection : esClassCollection, IEnumerable<Class>
	{
		public ClassCollection()
		{

		}
		
		public static implicit operator List<Class>(ClassCollection coll)
		{
			List<Class> list = new List<Class>();
			
			foreach (Class emp in coll)
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
				return  ClassMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClassQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Class(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Class();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ClassQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClassQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ClassQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Class AddNew()
		{
			Class entity = base.AddNewEntity() as Class;
			
			return entity;
		}

		public Class FindByPrimaryKey(System.String classID)
		{
			return base.FindByPrimaryKey(classID) as Class;
		}


		#region IEnumerable<Class> Members

		IEnumerator<Class> IEnumerable<Class>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Class;
			}
		}

		#endregion
		
		private ClassQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Class' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Class ({ClassID})")]
	[Serializable]
	public partial class Class : esClass
	{
		public Class()
		{

		}
	
		public Class(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClassMetadata.Meta();
			}
		}
		
		
		
		override protected esClassQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClassQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ClassQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClassQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ClassQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ClassQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ClassQuery : esClassQuery
	{
		public ClassQuery()
		{

		}		
		
		public ClassQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ClassQuery";
        }
		
			
	}


	[Serializable]
	public partial class ClassMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClassMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClassMetadata.ColumnNames.ClassID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.ClassName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassMetadata.PropertyNames.ClassName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.SRClassRL, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassMetadata.PropertyNames.SRClassRL;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.MarginPercentage, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClassMetadata.PropertyNames.MarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.DepositAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClassMetadata.PropertyNames.DepositAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.IsInPatientClass, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClassMetadata.PropertyNames.IsInPatientClass;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClassMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClassMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.Margin2Percentage, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClassMetadata.PropertyNames.Margin2Percentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.BpjsClassID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassMetadata.PropertyNames.BpjsClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.IsTariffClass, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClassMetadata.PropertyNames.IsTariffClass;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassMetadata.ColumnNames.ClassSeq, 13, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ClassMetadata.PropertyNames.ClassSeq;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ClassMetadata Meta()
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
			 public const string ClassID = "ClassID";
			 public const string ClassName = "ClassName";
			 public const string ShortName = "ShortName";
			 public const string SRClassRL = "SRClassRL";
			 public const string MarginPercentage = "MarginPercentage";
			 public const string DepositAmount = "DepositAmount";
			 public const string IsInPatientClass = "IsInPatientClass";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string Margin2Percentage = "Margin2Percentage";
			 public const string BpjsClassID = "BpjsClassID";
			 public const string IsTariffClass = "IsTariffClass";
			 public const string ClassSeq = "ClassSeq";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ClassID = "ClassID";
			 public const string ClassName = "ClassName";
			 public const string ShortName = "ShortName";
			 public const string SRClassRL = "SRClassRL";
			 public const string MarginPercentage = "MarginPercentage";
			 public const string DepositAmount = "DepositAmount";
			 public const string IsInPatientClass = "IsInPatientClass";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string Margin2Percentage = "Margin2Percentage";
			 public const string BpjsClassID = "BpjsClassID";
			 public const string IsTariffClass = "IsTariffClass";
			 public const string ClassSeq = "ClassSeq";
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
			lock (typeof(ClassMetadata))
			{
				if(ClassMetadata.mapDelegates == null)
				{
					ClassMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ClassMetadata.meta == null)
				{
					ClassMetadata.meta = new ClassMetadata();
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
				

				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClassRL", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DepositAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsInPatientClass", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Margin2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BpjsClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTariffClass", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClassSeq", new esTypeMap("smallint", "System.Int16"));			
				
				
				
				meta.Source = "Class";
				meta.Destination = "Class";
				
				meta.spInsert = "proc_ClassInsert";				
				meta.spUpdate = "proc_ClassUpdate";		
				meta.spDelete = "proc_ClassDelete";
				meta.spLoadAll = "proc_ClassLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClassLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClassMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
