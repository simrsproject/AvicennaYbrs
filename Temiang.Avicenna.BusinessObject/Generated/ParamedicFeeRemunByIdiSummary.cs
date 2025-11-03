/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/8/2020 6:11:00 AM
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
	abstract public class esParamedicFeeRemunByIdiSummaryCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeRemunByIdiSummaryCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeRemunByIdiSummaryCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiSummaryQuery query)
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
			this.InitQuery(query as esParamedicFeeRemunByIdiSummaryQuery);
		}
		#endregion
			
		virtual public ParamedicFeeRemunByIdiSummary DetachEntity(ParamedicFeeRemunByIdiSummary entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeRemunByIdiSummary;
		}
		
		virtual public ParamedicFeeRemunByIdiSummary AttachEntity(ParamedicFeeRemunByIdiSummary entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeRemunByIdiSummary;
		}
		
		virtual public void Combine(ParamedicFeeRemunByIdiSummaryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeRemunByIdiSummary this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeRemunByIdiSummary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeRemunByIdiSummary);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiSummary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeRemunByIdiSummaryQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeRemunByIdiSummary()
		{
		}
	
		public esParamedicFeeRemunByIdiSummary(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 remunID, String paramedicID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID, paramedicID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 remunID, String paramedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID, paramedicID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 remunID, String paramedicID)
		{
			esParamedicFeeRemunByIdiSummaryQuery query = this.GetDynamicQuery();
			query.Where(query.RemunID==remunID, query.ParamedicID==paramedicID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 remunID, String paramedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("RemunID",remunID);
			parms.Add("ParamedicID",paramedicID);
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
						case "RemunID": this.str.RemunID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "CoorporateGradeLevel": this.str.CoorporateGradeLevel = (string)value; break;
						case "CoorporateGradeValue": this.str.CoorporateGradeValue = (string)value; break;
						case "CoefficientSummary": this.str.CoefficientSummary = (string)value; break;
						case "ProcedureFeeValue": this.str.ProcedureFeeValue = (string)value; break;
						case "PositionFeeValue": this.str.PositionFeeValue = (string)value; break;
						case "InsentifFeeValue": this.str.InsentifFeeValue = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RemunID":
						
							if (value == null || value is System.Int32)
								this.RemunID = (System.Int32?)value;
							break;
						case "CoorporateGradeLevel":
						
							if (value == null || value is System.Int32)
								this.CoorporateGradeLevel = (System.Int32?)value;
							break;
						case "CoorporateGradeValue":
						
							if (value == null || value is System.Int32)
								this.CoorporateGradeValue = (System.Int32?)value;
							break;
						case "CoefficientSummary":
						
							if (value == null || value is System.Decimal)
								this.CoefficientSummary = (System.Decimal?)value;
							break;
						case "ProcedureFeeValue":
						
							if (value == null || value is System.Decimal)
								this.ProcedureFeeValue = (System.Decimal?)value;
							break;
						case "PositionFeeValue":
						
							if (value == null || value is System.Decimal)
								this.PositionFeeValue = (System.Decimal?)value;
							break;
						case "InsentifFeeValue":
						
							if (value == null || value is System.Decimal)
								this.InsentifFeeValue = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeRemunByIdiSummary.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.CoorporateGradeLevel
		/// </summary>
		virtual public System.Int32? CoorporateGradeLevel
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeLevel);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeLevel, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.CoorporateGradeValue
		/// </summary>
		virtual public System.Int32? CoorporateGradeValue
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeValue);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.CoefficientSummary
		/// </summary>
		virtual public System.Decimal? CoefficientSummary
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoefficientSummary);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoefficientSummary, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.ProcedureFeeValue
		/// </summary>
		virtual public System.Decimal? ProcedureFeeValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ProcedureFeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ProcedureFeeValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.PositionFeeValue
		/// </summary>
		virtual public System.Decimal? PositionFeeValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.PositionFeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.PositionFeeValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.InsentifFeeValue
		/// </summary>
		virtual public System.Decimal? InsentifFeeValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.InsentifFeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.InsentifFeeValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSummary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeRemunByIdiSummary entity)
			{
				this.entity = entity;
			}
			public System.String RemunID
			{
				get
				{
					System.Int32? data = entity.RemunID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunID = null;
					else entity.RemunID = Convert.ToInt32(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String CoorporateGradeLevel
			{
				get
				{
					System.Int32? data = entity.CoorporateGradeLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeLevel = null;
					else entity.CoorporateGradeLevel = Convert.ToInt32(value);
				}
			}
			public System.String CoorporateGradeValue
			{
				get
				{
					System.Int32? data = entity.CoorporateGradeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeValue = null;
					else entity.CoorporateGradeValue = Convert.ToInt32(value);
				}
			}
			public System.String CoefficientSummary
			{
				get
				{
					System.Decimal? data = entity.CoefficientSummary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoefficientSummary = null;
					else entity.CoefficientSummary = Convert.ToDecimal(value);
				}
			}
			public System.String ProcedureFeeValue
			{
				get
				{
					System.Decimal? data = entity.ProcedureFeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureFeeValue = null;
					else entity.ProcedureFeeValue = Convert.ToDecimal(value);
				}
			}
			public System.String PositionFeeValue
			{
				get
				{
					System.Decimal? data = entity.PositionFeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionFeeValue = null;
					else entity.PositionFeeValue = Convert.ToDecimal(value);
				}
			}
			public System.String InsentifFeeValue
			{
				get
				{
					System.Decimal? data = entity.InsentifFeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsentifFeeValue = null;
					else entity.InsentifFeeValue = Convert.ToDecimal(value);
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
			private esParamedicFeeRemunByIdiSummary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiSummaryQuery query)
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
				throw new Exception("esParamedicFeeRemunByIdiSummary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeRemunByIdiSummary : esParamedicFeeRemunByIdiSummary
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiSummaryQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiSummaryMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem CoorporateGradeLevel
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeLevel, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CoorporateGradeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeValue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CoefficientSummary
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoefficientSummary, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ProcedureFeeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ProcedureFeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PositionFeeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.PositionFeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem InsentifFeeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.InsentifFeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeRemunByIdiSummaryCollection")]
	public partial class ParamedicFeeRemunByIdiSummaryCollection : esParamedicFeeRemunByIdiSummaryCollection, IEnumerable< ParamedicFeeRemunByIdiSummary>
	{
		public ParamedicFeeRemunByIdiSummaryCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeRemunByIdiSummary>(ParamedicFeeRemunByIdiSummaryCollection coll)
		{
			List< ParamedicFeeRemunByIdiSummary> list = new List< ParamedicFeeRemunByIdiSummary>();
			
			foreach (ParamedicFeeRemunByIdiSummary emp in coll)
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
				return  ParamedicFeeRemunByIdiSummaryMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeRemunByIdiSummary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeRemunByIdiSummary();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiSummaryQuery();
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
		public bool Load(ParamedicFeeRemunByIdiSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeRemunByIdiSummary AddNew()
		{
			ParamedicFeeRemunByIdiSummary entity = base.AddNewEntity() as ParamedicFeeRemunByIdiSummary;
			
			return entity;		
		}
		public ParamedicFeeRemunByIdiSummary FindByPrimaryKey(Int32 remunID, String paramedicID)
		{
			return base.FindByPrimaryKey(remunID, paramedicID) as ParamedicFeeRemunByIdiSummary;
		}

		#region IEnumerable< ParamedicFeeRemunByIdiSummary> Members

		IEnumerator< ParamedicFeeRemunByIdiSummary> IEnumerable< ParamedicFeeRemunByIdiSummary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeRemunByIdiSummary;
			}
		}

		#endregion
		
		private ParamedicFeeRemunByIdiSummaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeRemunByIdiSummary' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeRemunByIdiSummary ({RemunID, ParamedicID})")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiSummary : esParamedicFeeRemunByIdiSummary
	{
		public ParamedicFeeRemunByIdiSummary()
		{
		}	
	
		public ParamedicFeeRemunByIdiSummary(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiSummaryMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeRemunByIdiSummaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiSummaryQuery();
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
		public bool Load(ParamedicFeeRemunByIdiSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeRemunByIdiSummaryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiSummaryQuery : esParamedicFeeRemunByIdiSummaryQuery
	{
		public ParamedicFeeRemunByIdiSummaryQuery()
		{

		}		
		
		public ParamedicFeeRemunByIdiSummaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeRemunByIdiSummaryQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeRemunByIdiSummaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeRemunByIdiSummaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.RemunID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.RemunID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeLevel, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.CoorporateGradeLevel;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoorporateGradeValue, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.CoorporateGradeValue;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CoefficientSummary, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.CoefficientSummary;
			c.NumericPrecision = 18;
			c.NumericScale = 9;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.ProcedureFeeValue, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.ProcedureFeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.PositionFeeValue, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.PositionFeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.InsentifFeeValue, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.InsentifFeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.CreateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSummaryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSummaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeRemunByIdiSummaryMetadata Meta()
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
			public const string RemunID = "RemunID";
			public const string ParamedicID = "ParamedicID";
			public const string CoorporateGradeLevel = "CoorporateGradeLevel";
			public const string CoorporateGradeValue = "CoorporateGradeValue";
			public const string CoefficientSummary = "CoefficientSummary";
			public const string ProcedureFeeValue = "ProcedureFeeValue";
			public const string PositionFeeValue = "PositionFeeValue";
			public const string InsentifFeeValue = "InsentifFeeValue";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunID = "RemunID";
			public const string ParamedicID = "ParamedicID";
			public const string CoorporateGradeLevel = "CoorporateGradeLevel";
			public const string CoorporateGradeValue = "CoorporateGradeValue";
			public const string CoefficientSummary = "CoefficientSummary";
			public const string ProcedureFeeValue = "ProcedureFeeValue";
			public const string PositionFeeValue = "PositionFeeValue";
			public const string InsentifFeeValue = "InsentifFeeValue";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(ParamedicFeeRemunByIdiSummaryMetadata))
			{
				if(ParamedicFeeRemunByIdiSummaryMetadata.mapDelegates == null)
				{
					ParamedicFeeRemunByIdiSummaryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeRemunByIdiSummaryMetadata.meta == null)
				{
					ParamedicFeeRemunByIdiSummaryMetadata.meta = new ParamedicFeeRemunByIdiSummaryMetadata();
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
				
				meta.AddTypeMap("RemunID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoorporateGradeLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoorporateGradeValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoefficientSummary", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ProcedureFeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PositionFeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InsentifFeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeeRemunByIdiSummary";
				meta.Destination = "ParamedicFeeRemunByIdiSummary";
				meta.spInsert = "proc_ParamedicFeeRemunByIdiSummaryInsert";				
				meta.spUpdate = "proc_ParamedicFeeRemunByIdiSummaryUpdate";		
				meta.spDelete = "proc_ParamedicFeeRemunByIdiSummaryDelete";
				meta.spLoadAll = "proc_ParamedicFeeRemunByIdiSummaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeRemunByIdiSummaryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeRemunByIdiSummaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
