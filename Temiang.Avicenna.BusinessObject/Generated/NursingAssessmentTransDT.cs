/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/31/2015 12:42:27 PM
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
	abstract public class esNursingAssessmentTransDTCollection : esEntityCollection
	{
		public esNursingAssessmentTransDTCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NursingAssessmentTransDTCollection";
		}

		#region Query Logic
		protected void InitQuery(esNursingAssessmentTransDTQuery query)
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
			this.InitQuery(query as esNursingAssessmentTransDTQuery);
		}
		#endregion
		
		virtual public NursingAssessmentTransDT DetachEntity(NursingAssessmentTransDT entity)
		{
			return base.DetachEntity(entity) as NursingAssessmentTransDT;
		}
		
		virtual public NursingAssessmentTransDT AttachEntity(NursingAssessmentTransDT entity)
		{
			return base.AttachEntity(entity) as NursingAssessmentTransDT;
		}
		
		virtual public void Combine(NursingAssessmentTransDTCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingAssessmentTransDT this[int index]
		{
			get
			{
				return base[index] as NursingAssessmentTransDT;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingAssessmentTransDT);
		}
	}



	[Serializable]
	abstract public class esNursingAssessmentTransDT : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingAssessmentTransDTQuery GetDynamicQuery()
		{
			return null;
		}

		public esNursingAssessmentTransDT()
		{

		}

		public esNursingAssessmentTransDT(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 id)
		{
			esNursingAssessmentTransDTQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 id)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",id);
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
						case "Id": this.str.Id = (string)value; break;							
						case "Hdid": this.str.Hdid = (string)value; break;							
						case "QuestionID": this.str.QuestionID = (string)value; break;							
						case "QuestionText": this.str.QuestionText = (string)value; break;							
						case "IsSubjective": this.str.IsSubjective = (string)value; break;							
						case "IsObjective": this.str.IsObjective = (string)value; break;							
						case "AnswerPrefix": this.str.AnswerPrefix = (string)value; break;							
						case "AnswerSuffix": this.str.AnswerSuffix = (string)value; break;							
						case "AnswerText": this.str.AnswerText = (string)value; break;							
						case "AnswerNum": this.str.AnswerNum = (string)value; break;							
						case "AnswerSelectionLineID": this.str.AnswerSelectionLineID = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
							break;
						
						case "Hdid":
						
							if (value == null || value is System.Int64)
								this.Hdid = (System.Int64?)value;
							break;
						
						case "IsSubjective":
						
							if (value == null || value is System.Boolean)
								this.IsSubjective = (System.Boolean?)value;
							break;
						
						case "IsObjective":
						
							if (value == null || value is System.Boolean)
								this.IsObjective = (System.Boolean?)value;
							break;
						
						case "AnswerNum":
						
							if (value == null || value is System.Decimal)
								this.AnswerNum = (System.Decimal?)value;
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
		/// Maps to NursingAssessmentTransDT.ID
		/// </summary>
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(NursingAssessmentTransDTMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt64(NursingAssessmentTransDTMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.HDID
		/// </summary>
		virtual public System.Int64? Hdid
		{
			get
			{
				return base.GetSystemInt64(NursingAssessmentTransDTMetadata.ColumnNames.Hdid);
			}
			
			set
			{
				base.SetSystemInt64(NursingAssessmentTransDTMetadata.ColumnNames.Hdid, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.QuestionID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.QuestionText
		/// </summary>
		virtual public System.String QuestionText
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.QuestionText);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.QuestionText, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.IsSubjective
		/// </summary>
		virtual public System.Boolean? IsSubjective
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentTransDTMetadata.ColumnNames.IsSubjective);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentTransDTMetadata.ColumnNames.IsSubjective, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.IsObjective
		/// </summary>
		virtual public System.Boolean? IsObjective
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentTransDTMetadata.ColumnNames.IsObjective);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentTransDTMetadata.ColumnNames.IsObjective, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.AnswerPrefix
		/// </summary>
		virtual public System.String AnswerPrefix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerPrefix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerPrefix, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.AnswerSuffix
		/// </summary>
		virtual public System.String AnswerSuffix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerSuffix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerSuffix, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.AnswerText
		/// </summary>
		virtual public System.String AnswerText
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerText);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerText, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.AnswerNum
		/// </summary>
		virtual public System.Decimal? AnswerNum
		{
			get
			{
				return base.GetSystemDecimal(NursingAssessmentTransDTMetadata.ColumnNames.AnswerNum);
			}
			
			set
			{
				base.SetSystemDecimal(NursingAssessmentTransDTMetadata.ColumnNames.AnswerNum, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.AnswerSelectionLineID
		/// </summary>
		virtual public System.String AnswerSelectionLineID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentTransDTMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentTransDTMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransDT.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esNursingAssessmentTransDT entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int64? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt64(value);
				}
			}
				
			public System.String Hdid
			{
				get
				{
					System.Int64? data = entity.Hdid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hdid = null;
					else entity.Hdid = Convert.ToInt64(value);
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
				
			public System.String QuestionText
			{
				get
				{
					System.String data = entity.QuestionText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionText = null;
					else entity.QuestionText = Convert.ToString(value);
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
				
			public System.String AnswerText
			{
				get
				{
					System.String data = entity.AnswerText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerText = null;
					else entity.AnswerText = Convert.ToString(value);
				}
			}
				
			public System.String AnswerNum
			{
				get
				{
					System.Decimal? data = entity.AnswerNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerNum = null;
					else entity.AnswerNum = Convert.ToDecimal(value);
				}
			}
				
			public System.String AnswerSelectionLineID
			{
				get
				{
					System.String data = entity.AnswerSelectionLineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerSelectionLineID = null;
					else entity.AnswerSelectionLineID = Convert.ToString(value);
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
			

			private esNursingAssessmentTransDT entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingAssessmentTransDTQuery query)
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
				throw new Exception("esNursingAssessmentTransDT can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNursingAssessmentTransDTQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentTransDTMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.Id, esSystemType.Int64);
			}
		} 
		
		public esQueryItem Hdid
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.Hdid, esSystemType.Int64);
			}
		} 
		
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionText
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.QuestionText, esSystemType.String);
			}
		} 
		
		public esQueryItem IsSubjective
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.IsSubjective, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsObjective
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.IsObjective, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AnswerPrefix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.AnswerPrefix, esSystemType.String);
			}
		} 
		
		public esQueryItem AnswerSuffix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.AnswerSuffix, esSystemType.String);
			}
		} 
		
		public esQueryItem AnswerText
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.AnswerText, esSystemType.String);
			}
		} 
		
		public esQueryItem AnswerNum
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.AnswerNum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AnswerSelectionLineID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingAssessmentTransDTCollection")]
	public partial class NursingAssessmentTransDTCollection : esNursingAssessmentTransDTCollection, IEnumerable<NursingAssessmentTransDT>
	{
		public NursingAssessmentTransDTCollection()
		{

		}
		
		public static implicit operator List<NursingAssessmentTransDT>(NursingAssessmentTransDTCollection coll)
		{
			List<NursingAssessmentTransDT> list = new List<NursingAssessmentTransDT>();
			
			foreach (NursingAssessmentTransDT emp in coll)
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
				return  NursingAssessmentTransDTMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentTransDTQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingAssessmentTransDT(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingAssessmentTransDT();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NursingAssessmentTransDTQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentTransDTQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NursingAssessmentTransDTQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NursingAssessmentTransDT AddNew()
		{
			NursingAssessmentTransDT entity = base.AddNewEntity() as NursingAssessmentTransDT;
			
			return entity;
		}

		public NursingAssessmentTransDT FindByPrimaryKey(System.Int64 id)
		{
			return base.FindByPrimaryKey(id) as NursingAssessmentTransDT;
		}


		#region IEnumerable<NursingAssessmentTransDT> Members

		IEnumerator<NursingAssessmentTransDT> IEnumerable<NursingAssessmentTransDT>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingAssessmentTransDT;
			}
		}

		#endregion
		
		private NursingAssessmentTransDTQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingAssessmentTransDT' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NursingAssessmentTransDT ({Id})")]
	[Serializable]
	public partial class NursingAssessmentTransDT : esNursingAssessmentTransDT
	{
		public NursingAssessmentTransDT()
		{

		}
	
		public NursingAssessmentTransDT(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentTransDTMetadata.Meta();
			}
		}
		
		
		
		override protected esNursingAssessmentTransDTQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentTransDTQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NursingAssessmentTransDTQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentTransDTQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NursingAssessmentTransDTQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NursingAssessmentTransDTQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NursingAssessmentTransDTQuery : esNursingAssessmentTransDTQuery
	{
		public NursingAssessmentTransDTQuery()
		{

		}		
		
		public NursingAssessmentTransDTQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NursingAssessmentTransDTQuery";
        }
		
			
	}


	[Serializable]
	public partial class NursingAssessmentTransDTMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingAssessmentTransDTMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.Hdid, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.Hdid;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.QuestionID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.QuestionID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.QuestionText, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.QuestionText;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.IsSubjective, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.IsSubjective;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.IsObjective, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.IsObjective;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.AnswerPrefix, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.AnswerPrefix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.AnswerSuffix, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.AnswerSuffix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.AnswerText, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.AnswerText;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.AnswerNum, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.AnswerNum;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.AnswerSelectionLineID;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.CreateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.CreateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentTransDTMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NursingAssessmentTransDTMetadata Meta()
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
			 public const string Id = "ID";
			 public const string Hdid = "HDID";
			 public const string QuestionID = "QuestionID";
			 public const string QuestionText = "QuestionText";
			 public const string IsSubjective = "IsSubjective";
			 public const string IsObjective = "IsObjective";
			 public const string AnswerPrefix = "AnswerPrefix";
			 public const string AnswerSuffix = "AnswerSuffix";
			 public const string AnswerText = "AnswerText";
			 public const string AnswerNum = "AnswerNum";
			 public const string AnswerSelectionLineID = "AnswerSelectionLineID";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Hdid = "Hdid";
			 public const string QuestionID = "QuestionID";
			 public const string QuestionText = "QuestionText";
			 public const string IsSubjective = "IsSubjective";
			 public const string IsObjective = "IsObjective";
			 public const string AnswerPrefix = "AnswerPrefix";
			 public const string AnswerSuffix = "AnswerSuffix";
			 public const string AnswerText = "AnswerText";
			 public const string AnswerNum = "AnswerNum";
			 public const string AnswerSelectionLineID = "AnswerSelectionLineID";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(NursingAssessmentTransDTMetadata))
			{
				if(NursingAssessmentTransDTMetadata.mapDelegates == null)
				{
					NursingAssessmentTransDTMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingAssessmentTransDTMetadata.meta == null)
				{
					NursingAssessmentTransDTMetadata.meta = new NursingAssessmentTransDTMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("Hdid", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSubjective", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsObjective", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AnswerPrefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerSuffix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerNum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AnswerSelectionLineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "NursingAssessmentTransDT";
				meta.Destination = "NursingAssessmentTransDT";
				
				meta.spInsert = "proc_NursingAssessmentTransDTInsert";				
				meta.spUpdate = "proc_NursingAssessmentTransDTUpdate";		
				meta.spDelete = "proc_NursingAssessmentTransDTDelete";
				meta.spLoadAll = "proc_NursingAssessmentTransDTLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingAssessmentTransDTLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingAssessmentTransDTMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
