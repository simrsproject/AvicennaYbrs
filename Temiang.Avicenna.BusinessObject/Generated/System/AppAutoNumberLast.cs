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
	abstract public class esAppAutoNumberLastCollection : esEntityCollectionWAuditLog
	{
		public esAppAutoNumberLastCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AppAutoNumberLastCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppAutoNumberLastQuery query)
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
			this.InitQuery(query as esAppAutoNumberLastQuery);
		}
		#endregion
		
		virtual public AppAutoNumberLast DetachEntity(AppAutoNumberLast entity)
		{
			return base.DetachEntity(entity) as AppAutoNumberLast;
		}
		
		virtual public AppAutoNumberLast AttachEntity(AppAutoNumberLast entity)
		{
			return base.AttachEntity(entity) as AppAutoNumberLast;
		}
		
		virtual public void Combine(AppAutoNumberLastCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppAutoNumberLast this[int index]
		{
			get
			{
				return base[index] as AppAutoNumberLast;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppAutoNumberLast);
		}
	}



	[Serializable]
	abstract public class esAppAutoNumberLast : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppAutoNumberLastQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppAutoNumberLast()
		{

		}

		public esAppAutoNumberLast(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRAutoNumber, System.DateTime effectiveDate, System.String departmentInitial, System.Int32 yearNo, System.Int32 monthNo, System.Int32 dayNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRAutoNumber, effectiveDate, departmentInitial, yearNo, monthNo, dayNo);
			else
				return LoadByPrimaryKeyStoredProcedure(sRAutoNumber, effectiveDate, departmentInitial, yearNo, monthNo, dayNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRAutoNumber, System.DateTime effectiveDate, System.String departmentInitial, System.Int32 yearNo, System.Int32 monthNo, System.Int32 dayNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRAutoNumber, effectiveDate, departmentInitial, yearNo, monthNo, dayNo);
			else
				return LoadByPrimaryKeyStoredProcedure(sRAutoNumber, effectiveDate, departmentInitial, yearNo, monthNo, dayNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRAutoNumber, System.DateTime effectiveDate, System.String departmentInitial, System.Int32 yearNo, System.Int32 monthNo, System.Int32 dayNo)
		{
			esAppAutoNumberLastQuery query = this.GetDynamicQuery();
			query.Where(query.SRAutoNumber == sRAutoNumber, query.EffectiveDate == effectiveDate, query.DepartmentInitial == departmentInitial, query.YearNo == yearNo, query.MonthNo == monthNo, query.DayNo == dayNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRAutoNumber, System.DateTime effectiveDate, System.String departmentInitial, System.Int32 yearNo, System.Int32 monthNo, System.Int32 dayNo)
		{
			esParameters parms = new esParameters();
			parms.Add("SRAutoNumber",sRAutoNumber);			parms.Add("EffectiveDate",effectiveDate);			parms.Add("DepartmentInitial",departmentInitial);			parms.Add("YearNo",yearNo);			parms.Add("MonthNo",monthNo);			parms.Add("DayNo",dayNo);
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
						case "SRAutoNumber": this.str.SRAutoNumber = (string)value; break;							
						case "EffectiveDate": this.str.EffectiveDate = (string)value; break;							
						case "DepartmentInitial": this.str.DepartmentInitial = (string)value; break;							
						case "YearNo": this.str.YearNo = (string)value; break;							
						case "MonthNo": this.str.MonthNo = (string)value; break;							
						case "DayNo": this.str.DayNo = (string)value; break;							
						case "LastNumber": this.str.LastNumber = (string)value; break;							
						case "LastCompleteNumber": this.str.LastCompleteNumber = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EffectiveDate":
						
							if (value == null || value is System.DateTime)
								this.EffectiveDate = (System.DateTime?)value;
							break;
						
						case "YearNo":
						
							if (value == null || value is System.Int32)
								this.YearNo = (System.Int32?)value;
							break;
						
						case "MonthNo":
						
							if (value == null || value is System.Int32)
								this.MonthNo = (System.Int32?)value;
							break;
						
						case "DayNo":
						
							if (value == null || value is System.Int32)
								this.DayNo = (System.Int32?)value;
							break;
						
						case "LastNumber":
						
							if (value == null || value is System.Int32)
								this.LastNumber = (System.Int32?)value;
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
		/// Maps to AppAutoNumberLast.SRAutoNumber
		/// </summary>
		virtual public System.String SRAutoNumber
		{
			get
			{
				return base.GetSystemString(AppAutoNumberLastMetadata.ColumnNames.SRAutoNumber);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberLastMetadata.ColumnNames.SRAutoNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.EffectiveDate
		/// </summary>
		virtual public System.DateTime? EffectiveDate
		{
			get
			{
				return base.GetSystemDateTime(AppAutoNumberLastMetadata.ColumnNames.EffectiveDate);
			}
			
			set
			{
				base.SetSystemDateTime(AppAutoNumberLastMetadata.ColumnNames.EffectiveDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.DepartmentInitial
		/// </summary>
		virtual public System.String DepartmentInitial
		{
			get
			{
				return base.GetSystemString(AppAutoNumberLastMetadata.ColumnNames.DepartmentInitial);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberLastMetadata.ColumnNames.DepartmentInitial, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.YearNo
		/// </summary>
		virtual public System.Int32? YearNo
		{
			get
			{
				return base.GetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.YearNo);
			}
			
			set
			{
				base.SetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.YearNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.MonthNo
		/// </summary>
		virtual public System.Int32? MonthNo
		{
			get
			{
				return base.GetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.MonthNo);
			}
			
			set
			{
				base.SetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.MonthNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.DayNo
		/// </summary>
		virtual public System.Int32? DayNo
		{
			get
			{
				return base.GetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.DayNo);
			}
			
			set
			{
				base.SetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.DayNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.LastNumber
		/// </summary>
		virtual public System.Int32? LastNumber
		{
			get
			{
				return base.GetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.LastNumber);
			}
			
			set
			{
				base.SetSystemInt32(AppAutoNumberLastMetadata.ColumnNames.LastNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.LastCompleteNumber
		/// </summary>
		virtual public System.String LastCompleteNumber
		{
			get
			{
				return base.GetSystemString(AppAutoNumberLastMetadata.ColumnNames.LastCompleteNumber);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberLastMetadata.ColumnNames.LastCompleteNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppAutoNumberLastMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppAutoNumberLastMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberLast.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppAutoNumberLastMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberLastMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAppAutoNumberLast entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRAutoNumber
			{
				get
				{
					System.String data = entity.SRAutoNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAutoNumber = null;
					else entity.SRAutoNumber = Convert.ToString(value);
				}
			}
				
			public System.String EffectiveDate
			{
				get
				{
					System.DateTime? data = entity.EffectiveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveDate = null;
					else entity.EffectiveDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String DepartmentInitial
			{
				get
				{
					System.String data = entity.DepartmentInitial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentInitial = null;
					else entity.DepartmentInitial = Convert.ToString(value);
				}
			}
				
			public System.String YearNo
			{
				get
				{
					System.Int32? data = entity.YearNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearNo = null;
					else entity.YearNo = Convert.ToInt32(value);
				}
			}
				
			public System.String MonthNo
			{
				get
				{
					System.Int32? data = entity.MonthNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthNo = null;
					else entity.MonthNo = Convert.ToInt32(value);
				}
			}
				
			public System.String DayNo
			{
				get
				{
					System.Int32? data = entity.DayNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DayNo = null;
					else entity.DayNo = Convert.ToInt32(value);
				}
			}
				
			public System.String LastNumber
			{
				get
				{
					System.Int32? data = entity.LastNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastNumber = null;
					else entity.LastNumber = Convert.ToInt32(value);
				}
			}
				
			public System.String LastCompleteNumber
			{
				get
				{
					System.String data = entity.LastCompleteNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCompleteNumber = null;
					else entity.LastCompleteNumber = Convert.ToString(value);
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
			

			private esAppAutoNumberLast entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppAutoNumberLastQuery query)
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
				throw new Exception("esAppAutoNumberLast can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AppAutoNumberLast : esAppAutoNumberLast
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
	abstract public class esAppAutoNumberLastQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppAutoNumberLastMetadata.Meta();
			}
		}	
		

		public esQueryItem SRAutoNumber
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.SRAutoNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem EffectiveDate
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.EffectiveDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem DepartmentInitial
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.DepartmentInitial, esSystemType.String);
			}
		} 
		
		public esQueryItem YearNo
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.YearNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MonthNo
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.MonthNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DayNo
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.DayNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastNumber
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.LastNumber, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastCompleteNumber
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.LastCompleteNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberLastMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppAutoNumberLastCollection")]
	public partial class AppAutoNumberLastCollection : esAppAutoNumberLastCollection, IEnumerable<AppAutoNumberLast>
	{
		public AppAutoNumberLastCollection()
		{

		}
		
		public static implicit operator List<AppAutoNumberLast>(AppAutoNumberLastCollection coll)
		{
			List<AppAutoNumberLast> list = new List<AppAutoNumberLast>();
			
			foreach (AppAutoNumberLast emp in coll)
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
				return  AppAutoNumberLastMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppAutoNumberLastQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppAutoNumberLast(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppAutoNumberLast();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AppAutoNumberLastQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppAutoNumberLastQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AppAutoNumberLastQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AppAutoNumberLast AddNew()
		{
			AppAutoNumberLast entity = base.AddNewEntity() as AppAutoNumberLast;
			
			return entity;
		}

		public AppAutoNumberLast FindByPrimaryKey(System.String sRAutoNumber, System.DateTime effectiveDate, System.String departmentInitial, System.Int32 yearNo, System.Int32 monthNo, System.Int32 dayNo)
		{
			return base.FindByPrimaryKey(sRAutoNumber, effectiveDate, departmentInitial, yearNo, monthNo, dayNo) as AppAutoNumberLast;
		}


		#region IEnumerable<AppAutoNumberLast> Members

		IEnumerator<AppAutoNumberLast> IEnumerable<AppAutoNumberLast>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppAutoNumberLast;
			}
		}

		#endregion
		
		private AppAutoNumberLastQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppAutoNumberLast' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AppAutoNumberLast ({SRAutoNumber},{EffectiveDate},{DepartmentInitial},{YearNo},{MonthNo},{DayNo})")]
	[Serializable]
	public partial class AppAutoNumberLast : esAppAutoNumberLast
	{
		public AppAutoNumberLast()
		{

		}
	
		public AppAutoNumberLast(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppAutoNumberLastMetadata.Meta();
			}
		}
		
		
		
		override protected esAppAutoNumberLastQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppAutoNumberLastQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AppAutoNumberLastQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppAutoNumberLastQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AppAutoNumberLastQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AppAutoNumberLastQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AppAutoNumberLastQuery : esAppAutoNumberLastQuery
	{
		public AppAutoNumberLastQuery()
		{

		}		
		
		public AppAutoNumberLastQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AppAutoNumberLastQuery";
        }
		
			
	}


	[Serializable]
	public partial class AppAutoNumberLastMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppAutoNumberLastMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.SRAutoNumber, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.SRAutoNumber;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.EffectiveDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.EffectiveDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.DepartmentInitial, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.DepartmentInitial;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.YearNo, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.YearNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.MonthNo, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.MonthNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.DayNo, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.DayNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.LastNumber, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.LastNumber;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.LastCompleteNumber, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.LastCompleteNumber;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberLastMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberLastMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AppAutoNumberLastMetadata Meta()
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
			 public const string SRAutoNumber = "SRAutoNumber";
			 public const string EffectiveDate = "EffectiveDate";
			 public const string DepartmentInitial = "DepartmentInitial";
			 public const string YearNo = "YearNo";
			 public const string MonthNo = "MonthNo";
			 public const string DayNo = "DayNo";
			 public const string LastNumber = "LastNumber";
			 public const string LastCompleteNumber = "LastCompleteNumber";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRAutoNumber = "SRAutoNumber";
			 public const string EffectiveDate = "EffectiveDate";
			 public const string DepartmentInitial = "DepartmentInitial";
			 public const string YearNo = "YearNo";
			 public const string MonthNo = "MonthNo";
			 public const string DayNo = "DayNo";
			 public const string LastNumber = "LastNumber";
			 public const string LastCompleteNumber = "LastCompleteNumber";
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
			lock (typeof(AppAutoNumberLastMetadata))
			{
				if(AppAutoNumberLastMetadata.mapDelegates == null)
				{
					AppAutoNumberLastMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppAutoNumberLastMetadata.meta == null)
				{
					AppAutoNumberLastMetadata.meta = new AppAutoNumberLastMetadata();
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
				

				meta.AddTypeMap("SRAutoNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EffectiveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DepartmentInitial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("YearNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MonthNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DayNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastCompleteNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AppAutoNumberLast";
				meta.Destination = "AppAutoNumberLast";
				
				meta.spInsert = "proc_AppAutoNumberLastInsert";				
				meta.spUpdate = "proc_AppAutoNumberLastUpdate";		
				meta.spDelete = "proc_AppAutoNumberLastDelete";
				meta.spLoadAll = "proc_AppAutoNumberLastLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppAutoNumberLastLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppAutoNumberLastMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
