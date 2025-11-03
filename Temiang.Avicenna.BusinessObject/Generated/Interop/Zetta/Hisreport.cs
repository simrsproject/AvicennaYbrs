/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : Oracle
Date Generated  : 5/31/2021 4:46:00 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Zetta
{

	[Serializable]
	abstract public class esHisreportCollection : esEntityCollectionWAuditLog
	{
		public esHisreportCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HisreportCollection";
		}

		#region Query Logic
		protected void InitQuery(esHisreportQuery query)
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
			this.InitQuery(query as esHisreportQuery);
		}
		#endregion
		
		virtual public Hisreport DetachEntity(Hisreport entity)
		{
			return base.DetachEntity(entity) as Hisreport;
		}
		
		virtual public Hisreport AttachEntity(Hisreport entity)
		{
			return base.AttachEntity(entity) as Hisreport;
		}
		
		virtual public void Combine(HisreportCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Hisreport this[int index]
		{
			get
			{
				return base[index] as Hisreport;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Hisreport);
		}
	}



	[Serializable]
	abstract public class esHisreport : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHisreportQuery GetDynamicQuery()
		{
			return null;
		}

		public esHisreport()
		{

		}

		public esHisreport(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Decimal reportKey)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reportKey);
			else
				return LoadByPrimaryKeyStoredProcedure(reportKey);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Decimal reportKey)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reportKey);
			else
				return LoadByPrimaryKeyStoredProcedure(reportKey);
		}

		private bool LoadByPrimaryKeyDynamic(System.Decimal reportKey)
		{
			esHisreportQuery query = this.GetDynamicQuery();
			query.Where(query.ReportKey == reportKey);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Decimal reportKey)
		{
			esParameters parms = new esParameters();
			parms.Add("REPORT_KEY",reportKey);
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
						case "ReportKey": this.str.ReportKey = (string)value; break;							
						case "CreateDate": this.str.CreateDate = (string)value; break;							
						case "Flag": this.str.Flag = (string)value; break;							
						case "AccessionNum": this.str.AccessionNum = (string)value; break;							
						case "ReportStat": this.str.ReportStat = (string)value; break;							
						case "DictateDocId": this.str.DictateDocId = (string)value; break;							
						case "DictateDocName": this.str.DictateDocName = (string)value; break;							
						case "DictateDate": this.str.DictateDate = (string)value; break;							
						case "ReadDocId": this.str.ReadDocId = (string)value; break;							
						case "ReadDocName": this.str.ReadDocName = (string)value; break;							
						case "ReadDate": this.str.ReadDate = (string)value; break;							
						case "ConfirmDocId": this.str.ConfirmDocId = (string)value; break;							
						case "ConfirmDocName": this.str.ConfirmDocName = (string)value; break;							
						case "ConfirmDate": this.str.ConfirmDate = (string)value; break;							
						case "AddconfirmDocId": this.str.AddconfirmDocId = (string)value; break;							
						case "AddconfirmDocName": this.str.AddconfirmDocName = (string)value; break;							
						case "AddconfirmDate": this.str.AddconfirmDate = (string)value; break;							
						case "ReportText": this.str.ReportText = (string)value; break;							
						case "Conclusion": this.str.Conclusion = (string)value; break;							
						case "ReportType": this.str.ReportType = (string)value; break;							
						case "Extension1": this.str.Extension1 = (string)value; break;							
						case "Extension2": this.str.Extension2 = (string)value; break;							
						case "Extension3": this.str.Extension3 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ReportKey":
						
							if (value == null || value is System.Decimal)
								this.ReportKey = (System.Decimal?)value;
							break;
						
						case "ReportStat":
						
							if (value == null || value is System.Decimal)
								this.ReportStat = (System.Decimal?)value;
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
		/// Maps to HISREPORT.REPORT_KEY
		/// </summary>
		virtual public System.Decimal? ReportKey
		{
			get
			{
				return base.GetSystemDecimal(HisreportMetadata.ColumnNames.ReportKey);
			}
			
			set
			{
				base.SetSystemDecimal(HisreportMetadata.ColumnNames.ReportKey, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.CREATE_DATE
		/// </summary>
		virtual public System.String CreateDate
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.CreateDate);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.CreateDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.FLAG
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.Flag, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.ACCESSION_NUM
		/// </summary>
		virtual public System.String AccessionNum
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.AccessionNum);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.AccessionNum, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.REPORT_STAT
		/// </summary>
		virtual public System.Decimal? ReportStat
		{
			get
			{
				return base.GetSystemDecimal(HisreportMetadata.ColumnNames.ReportStat);
			}
			
			set
			{
				base.SetSystemDecimal(HisreportMetadata.ColumnNames.ReportStat, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.DICTATE_DOC_ID
		/// </summary>
		virtual public System.String DictateDocId
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.DictateDocId);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.DictateDocId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.DICTATE_DOC_NAME
		/// </summary>
		virtual public System.String DictateDocName
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.DictateDocName);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.DictateDocName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.DICTATE_DATE
		/// </summary>
		virtual public System.String DictateDate
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.DictateDate);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.DictateDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.READ_DOC_ID
		/// </summary>
		virtual public System.String ReadDocId
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ReadDocId);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ReadDocId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.READ_DOC_NAME
		/// </summary>
		virtual public System.String ReadDocName
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ReadDocName);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ReadDocName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.READ_DATE
		/// </summary>
		virtual public System.String ReadDate
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ReadDate);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ReadDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.CONFIRM_DOC_ID
		/// </summary>
		virtual public System.String ConfirmDocId
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ConfirmDocId);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ConfirmDocId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.CONFIRM_DOC_NAME
		/// </summary>
		virtual public System.String ConfirmDocName
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ConfirmDocName);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ConfirmDocName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.CONFIRM_DATE
		/// </summary>
		virtual public System.String ConfirmDate
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ConfirmDate);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ConfirmDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.ADDCONFIRM_DOC_ID
		/// </summary>
		virtual public System.String AddconfirmDocId
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.AddconfirmDocId);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.AddconfirmDocId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.ADDCONFIRM_DOC_NAME
		/// </summary>
		virtual public System.String AddconfirmDocName
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.AddconfirmDocName);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.AddconfirmDocName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.ADDCONFIRM_DATE
		/// </summary>
		virtual public System.String AddconfirmDate
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.AddconfirmDate);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.AddconfirmDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.REPORT_TEXT
		/// </summary>
		virtual public System.String ReportText
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ReportText);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ReportText, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.CONCLUSION
		/// </summary>
		virtual public System.String Conclusion
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.Conclusion);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.Conclusion, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.REPORT_TYPE
		/// </summary>
		virtual public System.String ReportType
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.ReportType);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.ReportType, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.EXTENSION1
		/// </summary>
		virtual public System.String Extension1
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.Extension1);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.Extension1, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.EXTENSION2
		/// </summary>
		virtual public System.String Extension2
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.Extension2);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.Extension2, value);
			}
		}
		
		/// <summary>
		/// Maps to HISREPORT.EXTENSION3
		/// </summary>
		virtual public System.String Extension3
		{
			get
			{
				return base.GetSystemString(HisreportMetadata.ColumnNames.Extension3);
			}
			
			set
			{
				base.SetSystemString(HisreportMetadata.ColumnNames.Extension3, value);
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
			public esStrings(esHisreport entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ReportKey
			{
				get
				{
					System.Decimal? data = entity.ReportKey;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportKey = null;
					else entity.ReportKey = Convert.ToDecimal(value);
				}
			}
				
			public System.String CreateDate
			{
				get
				{
					System.String data = entity.CreateDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDate = null;
					else entity.CreateDate = Convert.ToString(value);
				}
			}
				
			public System.String Flag
			{
				get
				{
					System.String data = entity.Flag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Flag = null;
					else entity.Flag = Convert.ToString(value);
				}
			}
				
			public System.String AccessionNum
			{
				get
				{
					System.String data = entity.AccessionNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccessionNum = null;
					else entity.AccessionNum = Convert.ToString(value);
				}
			}
				
			public System.String ReportStat
			{
				get
				{
					System.Decimal? data = entity.ReportStat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportStat = null;
					else entity.ReportStat = Convert.ToDecimal(value);
				}
			}
				
			public System.String DictateDocId
			{
				get
				{
					System.String data = entity.DictateDocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DictateDocId = null;
					else entity.DictateDocId = Convert.ToString(value);
				}
			}
				
			public System.String DictateDocName
			{
				get
				{
					System.String data = entity.DictateDocName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DictateDocName = null;
					else entity.DictateDocName = Convert.ToString(value);
				}
			}
				
			public System.String DictateDate
			{
				get
				{
					System.String data = entity.DictateDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DictateDate = null;
					else entity.DictateDate = Convert.ToString(value);
				}
			}
				
			public System.String ReadDocId
			{
				get
				{
					System.String data = entity.ReadDocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReadDocId = null;
					else entity.ReadDocId = Convert.ToString(value);
				}
			}
				
			public System.String ReadDocName
			{
				get
				{
					System.String data = entity.ReadDocName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReadDocName = null;
					else entity.ReadDocName = Convert.ToString(value);
				}
			}
				
			public System.String ReadDate
			{
				get
				{
					System.String data = entity.ReadDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReadDate = null;
					else entity.ReadDate = Convert.ToString(value);
				}
			}
				
			public System.String ConfirmDocId
			{
				get
				{
					System.String data = entity.ConfirmDocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmDocId = null;
					else entity.ConfirmDocId = Convert.ToString(value);
				}
			}
				
			public System.String ConfirmDocName
			{
				get
				{
					System.String data = entity.ConfirmDocName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmDocName = null;
					else entity.ConfirmDocName = Convert.ToString(value);
				}
			}
				
			public System.String ConfirmDate
			{
				get
				{
					System.String data = entity.ConfirmDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmDate = null;
					else entity.ConfirmDate = Convert.ToString(value);
				}
			}
				
			public System.String AddconfirmDocId
			{
				get
				{
					System.String data = entity.AddconfirmDocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddconfirmDocId = null;
					else entity.AddconfirmDocId = Convert.ToString(value);
				}
			}
				
			public System.String AddconfirmDocName
			{
				get
				{
					System.String data = entity.AddconfirmDocName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddconfirmDocName = null;
					else entity.AddconfirmDocName = Convert.ToString(value);
				}
			}
				
			public System.String AddconfirmDate
			{
				get
				{
					System.String data = entity.AddconfirmDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddconfirmDate = null;
					else entity.AddconfirmDate = Convert.ToString(value);
				}
			}
				
			public System.String ReportText
			{
				get
				{
					System.String data = entity.ReportText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportText = null;
					else entity.ReportText = Convert.ToString(value);
				}
			}
				
			public System.String Conclusion
			{
				get
				{
					System.String data = entity.Conclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Conclusion = null;
					else entity.Conclusion = Convert.ToString(value);
				}
			}
				
			public System.String ReportType
			{
				get
				{
					System.String data = entity.ReportType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportType = null;
					else entity.ReportType = Convert.ToString(value);
				}
			}
				
			public System.String Extension1
			{
				get
				{
					System.String data = entity.Extension1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension1 = null;
					else entity.Extension1 = Convert.ToString(value);
				}
			}
				
			public System.String Extension2
			{
				get
				{
					System.String data = entity.Extension2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension2 = null;
					else entity.Extension2 = Convert.ToString(value);
				}
			}
				
			public System.String Extension3
			{
				get
				{
					System.String data = entity.Extension3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension3 = null;
					else entity.Extension3 = Convert.ToString(value);
				}
			}
			

			private esHisreport entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHisreportQuery query)
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
				throw new Exception("esHisreport can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esHisreportQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HisreportMetadata.Meta();
			}
		}	
		

		public esQueryItem ReportKey
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReportKey, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreateDate
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.CreateDate, esSystemType.String);
			}
		} 
		
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
		
		public esQueryItem AccessionNum
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.AccessionNum, esSystemType.String);
			}
		} 
		
		public esQueryItem ReportStat
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReportStat, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DictateDocId
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.DictateDocId, esSystemType.String);
			}
		} 
		
		public esQueryItem DictateDocName
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.DictateDocName, esSystemType.String);
			}
		} 
		
		public esQueryItem DictateDate
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.DictateDate, esSystemType.String);
			}
		} 
		
		public esQueryItem ReadDocId
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReadDocId, esSystemType.String);
			}
		} 
		
		public esQueryItem ReadDocName
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReadDocName, esSystemType.String);
			}
		} 
		
		public esQueryItem ReadDate
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReadDate, esSystemType.String);
			}
		} 
		
		public esQueryItem ConfirmDocId
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ConfirmDocId, esSystemType.String);
			}
		} 
		
		public esQueryItem ConfirmDocName
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ConfirmDocName, esSystemType.String);
			}
		} 
		
		public esQueryItem ConfirmDate
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ConfirmDate, esSystemType.String);
			}
		} 
		
		public esQueryItem AddconfirmDocId
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.AddconfirmDocId, esSystemType.String);
			}
		} 
		
		public esQueryItem AddconfirmDocName
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.AddconfirmDocName, esSystemType.String);
			}
		} 
		
		public esQueryItem AddconfirmDate
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.AddconfirmDate, esSystemType.String);
			}
		} 
		
		public esQueryItem ReportText
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReportText, esSystemType.String);
			}
		} 
		
		public esQueryItem Conclusion
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.Conclusion, esSystemType.String);
			}
		} 
		
		public esQueryItem ReportType
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.ReportType, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension1
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.Extension1, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension2
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.Extension2, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension3
		{
			get
			{
				return new esQueryItem(this, HisreportMetadata.ColumnNames.Extension3, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HisreportCollection")]
	public partial class HisreportCollection : esHisreportCollection, IEnumerable<Hisreport>
	{
		public HisreportCollection()
		{

		}
		
		public static implicit operator List<Hisreport>(HisreportCollection coll)
		{
			List<Hisreport> list = new List<Hisreport>();
			
			foreach (Hisreport emp in coll)
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
				return  HisreportMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HisreportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Hisreport(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Hisreport();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HisreportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HisreportQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HisreportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Hisreport AddNew()
		{
			Hisreport entity = base.AddNewEntity() as Hisreport;
			
			return entity;
		}

		public Hisreport FindByPrimaryKey(System.Decimal reportKey)
		{
			return base.FindByPrimaryKey(reportKey) as Hisreport;
		}


		#region IEnumerable<Hisreport> Members

		IEnumerator<Hisreport> IEnumerable<Hisreport>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Hisreport;
			}
		}

		#endregion
		
		private HisreportQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HISREPORT' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Hisreport ({ReportKey})")]
	[Serializable]
	public partial class Hisreport : esHisreport
	{
		public Hisreport()
		{

		}
	
		public Hisreport(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HisreportMetadata.Meta();
			}
		}
		
		
		
		override protected esHisreportQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HisreportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HisreportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HisreportQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HisreportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HisreportQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HisreportQuery : esHisreportQuery
	{
		public HisreportQuery()
		{

		}		
		
		public HisreportQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HisreportQuery";
        }
		
			
	}


	[Serializable]
	public partial class HisreportMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HisreportMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReportKey, 0, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = HisreportMetadata.PropertyNames.ReportKey;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 38;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.CreateDate, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.CreateDate;
			c.CharacterMaxLength = 14;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.Flag, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 14;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.AccessionNum, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.AccessionNum;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReportStat, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = HisreportMetadata.PropertyNames.ReportStat;
			c.NumericPrecision = 38;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.DictateDocId, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.DictateDocId;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.DictateDocName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.DictateDocName;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.DictateDate, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.DictateDate;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReadDocId, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ReadDocId;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReadDocName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ReadDocName;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReadDate, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ReadDate;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ConfirmDocId, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ConfirmDocId;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ConfirmDocName, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ConfirmDocName;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ConfirmDate, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ConfirmDate;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.AddconfirmDocId, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.AddconfirmDocId;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.AddconfirmDocName, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.AddconfirmDocName;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.AddconfirmDate, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.AddconfirmDate;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReportText, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ReportText;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.Conclusion, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.Conclusion;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.ReportType, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.ReportType;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.Extension1, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.Extension1;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.Extension2, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.Extension2;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisreportMetadata.ColumnNames.Extension3, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = HisreportMetadata.PropertyNames.Extension3;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HisreportMetadata Meta()
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
			 public const string ReportKey = "REPORT_KEY";
			 public const string CreateDate = "CREATE_DATE";
			 public const string Flag = "FLAG";
			 public const string AccessionNum = "ACCESSION_NUM";
			 public const string ReportStat = "REPORT_STAT";
			 public const string DictateDocId = "DICTATE_DOC_ID";
			 public const string DictateDocName = "DICTATE_DOC_NAME";
			 public const string DictateDate = "DICTATE_DATE";
			 public const string ReadDocId = "READ_DOC_ID";
			 public const string ReadDocName = "READ_DOC_NAME";
			 public const string ReadDate = "READ_DATE";
			 public const string ConfirmDocId = "CONFIRM_DOC_ID";
			 public const string ConfirmDocName = "CONFIRM_DOC_NAME";
			 public const string ConfirmDate = "CONFIRM_DATE";
			 public const string AddconfirmDocId = "ADDCONFIRM_DOC_ID";
			 public const string AddconfirmDocName = "ADDCONFIRM_DOC_NAME";
			 public const string AddconfirmDate = "ADDCONFIRM_DATE";
			 public const string ReportText = "REPORT_TEXT";
			 public const string Conclusion = "CONCLUSION";
			 public const string ReportType = "REPORT_TYPE";
			 public const string Extension1 = "EXTENSION1";
			 public const string Extension2 = "EXTENSION2";
			 public const string Extension3 = "EXTENSION3";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ReportKey = "ReportKey";
			 public const string CreateDate = "CreateDate";
			 public const string Flag = "Flag";
			 public const string AccessionNum = "AccessionNum";
			 public const string ReportStat = "ReportStat";
			 public const string DictateDocId = "DictateDocId";
			 public const string DictateDocName = "DictateDocName";
			 public const string DictateDate = "DictateDate";
			 public const string ReadDocId = "ReadDocId";
			 public const string ReadDocName = "ReadDocName";
			 public const string ReadDate = "ReadDate";
			 public const string ConfirmDocId = "ConfirmDocId";
			 public const string ConfirmDocName = "ConfirmDocName";
			 public const string ConfirmDate = "ConfirmDate";
			 public const string AddconfirmDocId = "AddconfirmDocId";
			 public const string AddconfirmDocName = "AddconfirmDocName";
			 public const string AddconfirmDate = "AddconfirmDate";
			 public const string ReportText = "ReportText";
			 public const string Conclusion = "Conclusion";
			 public const string ReportType = "ReportType";
			 public const string Extension1 = "Extension1";
			 public const string Extension2 = "Extension2";
			 public const string Extension3 = "Extension3";
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
			lock (typeof(HisreportMetadata))
			{
				if(HisreportMetadata.mapDelegates == null)
				{
					HisreportMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HisreportMetadata.meta == null)
				{
					HisreportMetadata.meta = new HisreportMetadata();
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
				

				meta.AddTypeMap("ReportKey", new esTypeMap("NUMBER", "System.Decimal"));
				meta.AddTypeMap("CreateDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("AccessionNum", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ReportStat", new esTypeMap("NUMBER", "System.Decimal"));
				meta.AddTypeMap("DictateDocId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("DictateDocName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("DictateDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ReadDocId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ReadDocName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ReadDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ConfirmDocId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ConfirmDocName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ConfirmDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("AddconfirmDocId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("AddconfirmDocName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("AddconfirmDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ReportText", new esTypeMap("CLOB", "System.String"));
				meta.AddTypeMap("Conclusion", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ReportType", new esTypeMap("CHAR", "System.String"));
				meta.AddTypeMap("Extension1", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension2", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension3", new esTypeMap("VARCHAR2", "System.String"));			
				
				
				
				meta.Source = "HISREPORT";
				meta.Destination = "HISREPORT";
				
				meta.spInsert = "proc_HISREPORTInsert";				
				meta.spUpdate = "proc_HISREPORTUpdate";		
				meta.spDelete = "proc_HISREPORTDelete";
				meta.spLoadAll = "proc_HISREPORTLoadAll";
				meta.spLoadByPrimaryKey = "proc_HISREPORTLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HisreportMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
