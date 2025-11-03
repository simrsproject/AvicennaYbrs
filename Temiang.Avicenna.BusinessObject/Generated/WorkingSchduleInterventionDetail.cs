/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/19/2021 8:59:25 AM
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
	abstract public class esWorkingSchduleInterventionDetailCollection : esEntityCollectionWAuditLog
	{
		public esWorkingSchduleInterventionDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingSchduleInterventionDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingSchduleInterventionDetailQuery query)
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
			this.InitQuery(query as esWorkingSchduleInterventionDetailQuery);
		}
		#endregion
		
		virtual public WorkingSchduleInterventionDetail DetachEntity(WorkingSchduleInterventionDetail entity)
		{
			return base.DetachEntity(entity) as WorkingSchduleInterventionDetail;
		}
		
		virtual public WorkingSchduleInterventionDetail AttachEntity(WorkingSchduleInterventionDetail entity)
		{
			return base.AttachEntity(entity) as WorkingSchduleInterventionDetail;
		}
		
		virtual public void Combine(WorkingSchduleInterventionDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingSchduleInterventionDetail this[int index]
		{
			get
			{
				return base[index] as WorkingSchduleInterventionDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingSchduleInterventionDetail);
		}
	}



	[Serializable]
	abstract public class esWorkingSchduleInterventionDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingSchduleInterventionDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingSchduleInterventionDetail()
		{

		}

		public esWorkingSchduleInterventionDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 workingSchduleInterventionDetailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingSchduleInterventionDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingSchduleInterventionDetailID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 workingSchduleInterventionDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingSchduleInterventionDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingSchduleInterventionDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 workingSchduleInterventionDetailID)
		{
			esWorkingSchduleInterventionDetailQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingSchduleInterventionDetailID == workingSchduleInterventionDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 workingSchduleInterventionDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingSchduleInterventionDetailID",workingSchduleInterventionDetailID);
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
						case "WorkingSchduleInterventionDetailID": this.str.WorkingSchduleInterventionDetailID = (string)value; break;							
						case "WorkingSchduleInterventionID": this.str.WorkingSchduleInterventionID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "WorkingHourIDDay1": this.str.WorkingHourIDDay1 = (string)value; break;							
						case "WorkingHourIDDay2": this.str.WorkingHourIDDay2 = (string)value; break;							
						case "WorkingHourIDDay3": this.str.WorkingHourIDDay3 = (string)value; break;							
						case "WorkingHourIDDay4": this.str.WorkingHourIDDay4 = (string)value; break;							
						case "WorkingHourIDDay5": this.str.WorkingHourIDDay5 = (string)value; break;							
						case "WorkingHourIDDay6": this.str.WorkingHourIDDay6 = (string)value; break;							
						case "WorkingHourIDDay7": this.str.WorkingHourIDDay7 = (string)value; break;							
						case "WorkingHourIDDay8": this.str.WorkingHourIDDay8 = (string)value; break;							
						case "WorkingHourIDDay9": this.str.WorkingHourIDDay9 = (string)value; break;							
						case "WorkingHourIDDay10": this.str.WorkingHourIDDay10 = (string)value; break;							
						case "WorkingHourIDDay11": this.str.WorkingHourIDDay11 = (string)value; break;							
						case "WorkingHourIDDay12": this.str.WorkingHourIDDay12 = (string)value; break;							
						case "WorkingHourIDDay13": this.str.WorkingHourIDDay13 = (string)value; break;							
						case "WorkingHourIDDay14": this.str.WorkingHourIDDay14 = (string)value; break;							
						case "WorkingHourIDDay15": this.str.WorkingHourIDDay15 = (string)value; break;							
						case "WorkingHourIDDay16": this.str.WorkingHourIDDay16 = (string)value; break;							
						case "WorkingHourIDDay17": this.str.WorkingHourIDDay17 = (string)value; break;							
						case "WorkingHourIDDay18": this.str.WorkingHourIDDay18 = (string)value; break;							
						case "WorkingHourIDDay19": this.str.WorkingHourIDDay19 = (string)value; break;							
						case "WorkingHourIDDay20": this.str.WorkingHourIDDay20 = (string)value; break;							
						case "WorkingHourIDDay21": this.str.WorkingHourIDDay21 = (string)value; break;							
						case "WorkingHourIDDay22": this.str.WorkingHourIDDay22 = (string)value; break;							
						case "WorkingHourIDDay23": this.str.WorkingHourIDDay23 = (string)value; break;							
						case "WorkingHourIDDay24": this.str.WorkingHourIDDay24 = (string)value; break;							
						case "WorkingHourIDDay25": this.str.WorkingHourIDDay25 = (string)value; break;							
						case "WorkingHourIDDay26": this.str.WorkingHourIDDay26 = (string)value; break;							
						case "WorkingHourIDDay27": this.str.WorkingHourIDDay27 = (string)value; break;							
						case "WorkingHourIDDay28": this.str.WorkingHourIDDay28 = (string)value; break;							
						case "WorkingHourIDDay29": this.str.WorkingHourIDDay29 = (string)value; break;							
						case "WorkingHourIDDay30": this.str.WorkingHourIDDay30 = (string)value; break;							
						case "WorkingHourIDDay31": this.str.WorkingHourIDDay31 = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WorkingSchduleInterventionDetailID":
						
							if (value == null || value is System.Int32)
								this.WorkingSchduleInterventionDetailID = (System.Int32?)value;
							break;
						
						case "WorkingSchduleInterventionID":
						
							if (value == null || value is System.Int32)
								this.WorkingSchduleInterventionID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay1":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay1 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay2":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay2 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay3":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay3 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay4":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay4 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay5":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay5 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay6":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay6 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay7":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay7 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay8":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay8 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay9":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay9 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay10":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay10 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay11":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay11 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay12":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay12 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay13":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay13 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay14":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay14 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay15":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay15 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay16":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay16 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay17":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay17 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay18":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay18 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay19":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay19 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay20":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay20 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay21":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay21 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay22":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay22 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay23":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay23 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay24":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay24 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay25":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay25 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay26":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay26 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay27":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay27 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay28":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay28 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay29":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay29 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay30":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay30 = (System.Int32?)value;
							break;
						
						case "WorkingHourIDDay31":
						
							if (value == null || value is System.Int32)
								this.WorkingHourIDDay31 = (System.Int32?)value;
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
		/// Maps to WorkingSchduleInterventionDetail.WorkingSchduleInterventionDetailID
		/// </summary>
		virtual public System.Int32? WorkingSchduleInterventionDetailID
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionDetailID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingSchduleInterventionID
		/// </summary>
		virtual public System.Int32? WorkingSchduleInterventionID
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay1
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay1
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay1);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay1, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay2
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay2
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay2);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay2, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay3
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay3
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay3);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay3, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay4
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay4
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay4);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay4, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay5
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay5
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay5);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay5, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay6
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay6
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay6);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay6, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay7
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay7
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay7);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay7, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay8
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay8
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay8);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay8, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay9
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay9
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay9);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay9, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay10
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay10
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay10);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay10, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay11
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay11
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay11);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay11, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay12
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay12
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay12);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay12, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay13
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay13
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay13);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay13, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay14
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay14
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay14);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay14, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay15
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay15
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay15);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay15, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay16
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay16
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay16);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay16, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay17
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay17
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay17);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay17, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay18
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay18
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay18);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay18, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay19
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay19
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay19);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay19, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay20
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay20
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay20);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay20, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay21
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay21
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay21);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay21, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay22
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay22
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay22);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay22, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay23
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay23
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay23);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay23, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay24
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay24
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay24);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay24, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay25
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay25
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay25);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay25, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay26
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay26
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay26);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay26, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay27
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay27
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay27);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay27, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay28
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay28
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay28);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay28, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay29
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay29
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay29);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay29, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay30
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay30
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay30);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay30, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.WorkingHourIDDay31
		/// </summary>
		virtual public System.Int32? WorkingHourIDDay31
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay31);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay31, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleInterventionDetail.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esWorkingSchduleInterventionDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WorkingSchduleInterventionDetailID
			{
				get
				{
					System.Int32? data = entity.WorkingSchduleInterventionDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingSchduleInterventionDetailID = null;
					else entity.WorkingSchduleInterventionDetailID = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingSchduleInterventionID
			{
				get
				{
					System.Int32? data = entity.WorkingSchduleInterventionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingSchduleInterventionID = null;
					else entity.WorkingSchduleInterventionID = Convert.ToInt32(value);
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
				
			public System.String WorkingHourIDDay1
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay1 = null;
					else entity.WorkingHourIDDay1 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay2
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay2 = null;
					else entity.WorkingHourIDDay2 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay3
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay3 = null;
					else entity.WorkingHourIDDay3 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay4
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay4 = null;
					else entity.WorkingHourIDDay4 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay5
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay5 = null;
					else entity.WorkingHourIDDay5 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay6
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay6 = null;
					else entity.WorkingHourIDDay6 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay7
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay7 = null;
					else entity.WorkingHourIDDay7 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay8
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay8 = null;
					else entity.WorkingHourIDDay8 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay9
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay9;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay9 = null;
					else entity.WorkingHourIDDay9 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay10
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay10 = null;
					else entity.WorkingHourIDDay10 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay11
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay11;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay11 = null;
					else entity.WorkingHourIDDay11 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay12
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay12;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay12 = null;
					else entity.WorkingHourIDDay12 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay13
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay13;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay13 = null;
					else entity.WorkingHourIDDay13 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay14
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay14;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay14 = null;
					else entity.WorkingHourIDDay14 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay15
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay15;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay15 = null;
					else entity.WorkingHourIDDay15 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay16
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay16;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay16 = null;
					else entity.WorkingHourIDDay16 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay17
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay17;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay17 = null;
					else entity.WorkingHourIDDay17 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay18
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay18;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay18 = null;
					else entity.WorkingHourIDDay18 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay19
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay19;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay19 = null;
					else entity.WorkingHourIDDay19 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay20
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay20;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay20 = null;
					else entity.WorkingHourIDDay20 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay21
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay21;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay21 = null;
					else entity.WorkingHourIDDay21 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay22
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay22;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay22 = null;
					else entity.WorkingHourIDDay22 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay23
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay23;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay23 = null;
					else entity.WorkingHourIDDay23 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay24
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay24;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay24 = null;
					else entity.WorkingHourIDDay24 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay25
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay25;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay25 = null;
					else entity.WorkingHourIDDay25 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay26
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay26;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay26 = null;
					else entity.WorkingHourIDDay26 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay27
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay27;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay27 = null;
					else entity.WorkingHourIDDay27 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay28
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay28;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay28 = null;
					else entity.WorkingHourIDDay28 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay29
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay29;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay29 = null;
					else entity.WorkingHourIDDay29 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay30
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay30 = null;
					else entity.WorkingHourIDDay30 = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourIDDay31
			{
				get
				{
					System.Int32? data = entity.WorkingHourIDDay31;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourIDDay31 = null;
					else entity.WorkingHourIDDay31 = Convert.ToInt32(value);
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
				
			public System.String LastUpdateUserID
			{
				get
				{
					System.String data = entity.LastUpdateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
					else entity.LastUpdateUserID = Convert.ToString(value);
				}
			}
			

			private esWorkingSchduleInterventionDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingSchduleInterventionDetailQuery query)
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
				throw new Exception("esWorkingSchduleInterventionDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esWorkingSchduleInterventionDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingSchduleInterventionDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingSchduleInterventionDetailID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionDetailID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingSchduleInterventionID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay1
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay1, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay2
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay2, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay3
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay3, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay4
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay4, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay5
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay5, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay6
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay6, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay7
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay7, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay8
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay8, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay9
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay9, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay10
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay10, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay11
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay11, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay12
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay12, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay13
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay13, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay14
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay14, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay15
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay15, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay16
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay16, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay17
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay17, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay18
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay18, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay19
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay19, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay20
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay20, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay21
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay21, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay22
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay22, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay23
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay23, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay24
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay24, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay25
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay25, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay26
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay26, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay27
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay27, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay28
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay28, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay29
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay29, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay30
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay30, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourIDDay31
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay31, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingSchduleInterventionDetailCollection")]
	public partial class WorkingSchduleInterventionDetailCollection : esWorkingSchduleInterventionDetailCollection, IEnumerable<WorkingSchduleInterventionDetail>
	{
		public WorkingSchduleInterventionDetailCollection()
		{

		}
		
		public static implicit operator List<WorkingSchduleInterventionDetail>(WorkingSchduleInterventionDetailCollection coll)
		{
			List<WorkingSchduleInterventionDetail> list = new List<WorkingSchduleInterventionDetail>();
			
			foreach (WorkingSchduleInterventionDetail emp in coll)
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
				return  WorkingSchduleInterventionDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingSchduleInterventionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingSchduleInterventionDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingSchduleInterventionDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingSchduleInterventionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingSchduleInterventionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingSchduleInterventionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingSchduleInterventionDetail AddNew()
		{
			WorkingSchduleInterventionDetail entity = base.AddNewEntity() as WorkingSchduleInterventionDetail;
			
			return entity;
		}

		public WorkingSchduleInterventionDetail FindByPrimaryKey(System.Int32 workingSchduleInterventionDetailID)
		{
			return base.FindByPrimaryKey(workingSchduleInterventionDetailID) as WorkingSchduleInterventionDetail;
		}


		#region IEnumerable<WorkingSchduleInterventionDetail> Members

		IEnumerator<WorkingSchduleInterventionDetail> IEnumerable<WorkingSchduleInterventionDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingSchduleInterventionDetail;
			}
		}

		#endregion
		
		private WorkingSchduleInterventionDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingSchduleInterventionDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingSchduleInterventionDetail ({WorkingSchduleInterventionDetailID})")]
	[Serializable]
	public partial class WorkingSchduleInterventionDetail : esWorkingSchduleInterventionDetail
	{
		public WorkingSchduleInterventionDetail()
		{

		}
	
		public WorkingSchduleInterventionDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingSchduleInterventionDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingSchduleInterventionDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingSchduleInterventionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingSchduleInterventionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingSchduleInterventionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingSchduleInterventionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingSchduleInterventionDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingSchduleInterventionDetailQuery : esWorkingSchduleInterventionDetailQuery
	{
		public WorkingSchduleInterventionDetailQuery()
		{

		}		
		
		public WorkingSchduleInterventionDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingSchduleInterventionDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingSchduleInterventionDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingSchduleInterventionDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingSchduleInterventionDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingSchduleInterventionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingSchduleInterventionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay1, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay1;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay2, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay3, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay3;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay4, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay4;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay5, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay5;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay6, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay6;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay7, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay7;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay8, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay8;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay9, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay9;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay10, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay10;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay11, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay11;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay12, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay12;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay13, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay13;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay14, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay14;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay15, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay15;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay16, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay16;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay17, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay17;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay18, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay18;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay19, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay19;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay20, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay20;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay21, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay21;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay22, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay22;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay23, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay23;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay24, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay24;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay25, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay25;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay26, 28, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay26;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay27, 29, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay27;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay28, 30, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay28;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay29, 31, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay29;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay30, 32, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay30;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay31, 33, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.WorkingHourIDDay31;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateDateTime, 34, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionDetailMetadata.ColumnNames.LastUpdateUserID, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingSchduleInterventionDetailMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingSchduleInterventionDetailMetadata Meta()
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
			 public const string WorkingSchduleInterventionDetailID = "WorkingSchduleInterventionDetailID";
			 public const string WorkingSchduleInterventionID = "WorkingSchduleInterventionID";
			 public const string PersonID = "PersonID";
			 public const string WorkingHourIDDay1 = "WorkingHourIDDay1";
			 public const string WorkingHourIDDay2 = "WorkingHourIDDay2";
			 public const string WorkingHourIDDay3 = "WorkingHourIDDay3";
			 public const string WorkingHourIDDay4 = "WorkingHourIDDay4";
			 public const string WorkingHourIDDay5 = "WorkingHourIDDay5";
			 public const string WorkingHourIDDay6 = "WorkingHourIDDay6";
			 public const string WorkingHourIDDay7 = "WorkingHourIDDay7";
			 public const string WorkingHourIDDay8 = "WorkingHourIDDay8";
			 public const string WorkingHourIDDay9 = "WorkingHourIDDay9";
			 public const string WorkingHourIDDay10 = "WorkingHourIDDay10";
			 public const string WorkingHourIDDay11 = "WorkingHourIDDay11";
			 public const string WorkingHourIDDay12 = "WorkingHourIDDay12";
			 public const string WorkingHourIDDay13 = "WorkingHourIDDay13";
			 public const string WorkingHourIDDay14 = "WorkingHourIDDay14";
			 public const string WorkingHourIDDay15 = "WorkingHourIDDay15";
			 public const string WorkingHourIDDay16 = "WorkingHourIDDay16";
			 public const string WorkingHourIDDay17 = "WorkingHourIDDay17";
			 public const string WorkingHourIDDay18 = "WorkingHourIDDay18";
			 public const string WorkingHourIDDay19 = "WorkingHourIDDay19";
			 public const string WorkingHourIDDay20 = "WorkingHourIDDay20";
			 public const string WorkingHourIDDay21 = "WorkingHourIDDay21";
			 public const string WorkingHourIDDay22 = "WorkingHourIDDay22";
			 public const string WorkingHourIDDay23 = "WorkingHourIDDay23";
			 public const string WorkingHourIDDay24 = "WorkingHourIDDay24";
			 public const string WorkingHourIDDay25 = "WorkingHourIDDay25";
			 public const string WorkingHourIDDay26 = "WorkingHourIDDay26";
			 public const string WorkingHourIDDay27 = "WorkingHourIDDay27";
			 public const string WorkingHourIDDay28 = "WorkingHourIDDay28";
			 public const string WorkingHourIDDay29 = "WorkingHourIDDay29";
			 public const string WorkingHourIDDay30 = "WorkingHourIDDay30";
			 public const string WorkingHourIDDay31 = "WorkingHourIDDay31";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingSchduleInterventionDetailID = "WorkingSchduleInterventionDetailID";
			 public const string WorkingSchduleInterventionID = "WorkingSchduleInterventionID";
			 public const string PersonID = "PersonID";
			 public const string WorkingHourIDDay1 = "WorkingHourIDDay1";
			 public const string WorkingHourIDDay2 = "WorkingHourIDDay2";
			 public const string WorkingHourIDDay3 = "WorkingHourIDDay3";
			 public const string WorkingHourIDDay4 = "WorkingHourIDDay4";
			 public const string WorkingHourIDDay5 = "WorkingHourIDDay5";
			 public const string WorkingHourIDDay6 = "WorkingHourIDDay6";
			 public const string WorkingHourIDDay7 = "WorkingHourIDDay7";
			 public const string WorkingHourIDDay8 = "WorkingHourIDDay8";
			 public const string WorkingHourIDDay9 = "WorkingHourIDDay9";
			 public const string WorkingHourIDDay10 = "WorkingHourIDDay10";
			 public const string WorkingHourIDDay11 = "WorkingHourIDDay11";
			 public const string WorkingHourIDDay12 = "WorkingHourIDDay12";
			 public const string WorkingHourIDDay13 = "WorkingHourIDDay13";
			 public const string WorkingHourIDDay14 = "WorkingHourIDDay14";
			 public const string WorkingHourIDDay15 = "WorkingHourIDDay15";
			 public const string WorkingHourIDDay16 = "WorkingHourIDDay16";
			 public const string WorkingHourIDDay17 = "WorkingHourIDDay17";
			 public const string WorkingHourIDDay18 = "WorkingHourIDDay18";
			 public const string WorkingHourIDDay19 = "WorkingHourIDDay19";
			 public const string WorkingHourIDDay20 = "WorkingHourIDDay20";
			 public const string WorkingHourIDDay21 = "WorkingHourIDDay21";
			 public const string WorkingHourIDDay22 = "WorkingHourIDDay22";
			 public const string WorkingHourIDDay23 = "WorkingHourIDDay23";
			 public const string WorkingHourIDDay24 = "WorkingHourIDDay24";
			 public const string WorkingHourIDDay25 = "WorkingHourIDDay25";
			 public const string WorkingHourIDDay26 = "WorkingHourIDDay26";
			 public const string WorkingHourIDDay27 = "WorkingHourIDDay27";
			 public const string WorkingHourIDDay28 = "WorkingHourIDDay28";
			 public const string WorkingHourIDDay29 = "WorkingHourIDDay29";
			 public const string WorkingHourIDDay30 = "WorkingHourIDDay30";
			 public const string WorkingHourIDDay31 = "WorkingHourIDDay31";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
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
			lock (typeof(WorkingSchduleInterventionDetailMetadata))
			{
				if(WorkingSchduleInterventionDetailMetadata.mapDelegates == null)
				{
					WorkingSchduleInterventionDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingSchduleInterventionDetailMetadata.meta == null)
				{
					WorkingSchduleInterventionDetailMetadata.meta = new WorkingSchduleInterventionDetailMetadata();
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
				

				meta.AddTypeMap("WorkingSchduleInterventionDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingSchduleInterventionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay1", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay3", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay4", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay5", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay6", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay7", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay8", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay9", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay10", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay11", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay12", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay13", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay14", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay15", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay16", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay17", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay18", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay19", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay20", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay21", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay22", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay23", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay24", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay25", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay26", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay27", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay28", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay29", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay30", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourIDDay31", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "WorkingSchduleInterventionDetail";
				meta.Destination = "WorkingSchduleInterventionDetail";
				
				meta.spInsert = "proc_WorkingSchduleInterventionDetailInsert";				
				meta.spUpdate = "proc_WorkingSchduleInterventionDetailUpdate";		
				meta.spDelete = "proc_WorkingSchduleInterventionDetailDelete";
				meta.spLoadAll = "proc_WorkingSchduleInterventionDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingSchduleInterventionDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingSchduleInterventionDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
