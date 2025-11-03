/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/1/2022 11:48:48 AM
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
	abstract public class esAppProgramCollection : esEntityCollectionWAuditLog
	{
		public esAppProgramCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppProgramCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppProgramQuery query)
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
			this.InitQuery(query as esAppProgramQuery);
		}
		#endregion
			
		virtual public AppProgram DetachEntity(AppProgram entity)
		{
			return base.DetachEntity(entity) as AppProgram;
		}
		
		virtual public AppProgram AttachEntity(AppProgram entity)
		{
			return base.AttachEntity(entity) as AppProgram;
		}
		
		virtual public void Combine(AppProgramCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppProgram this[int index]
		{
			get
			{
				return base[index] as AppProgram;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppProgram);
		}
	}

	[Serializable]
	abstract public class esAppProgram : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppProgramQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppProgram()
		{
		}
	
		public esAppProgram(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String programID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String programID)
		{
			esAppProgramQuery query = this.GetDynamicQuery();
			query.Where(query.ProgramID == programID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String programID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProgramID",programID);
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
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "ParentProgramID": this.str.ParentProgramID = (string)value; break;
						case "ProgramName": this.str.ProgramName = (string)value; break;
						case "TopLevelProgramID": this.str.TopLevelProgramID = (string)value; break;
						case "RootLevel": this.str.RootLevel = (string)value; break;
						case "RowIndex": this.str.RowIndex = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsParentProgram": this.str.IsParentProgram = (string)value; break;
						case "IsProgram": this.str.IsProgram = (string)value; break;
						case "IsBeginGroup": this.str.IsBeginGroup = (string)value; break;
						case "ProgramType": this.str.ProgramType = (string)value; break;
						case "IsProgramAddAble": this.str.IsProgramAddAble = (string)value; break;
						case "IsProgramEditAble": this.str.IsProgramEditAble = (string)value; break;
						case "IsProgramDeleteAble": this.str.IsProgramDeleteAble = (string)value; break;
						case "IsProgramViewAble": this.str.IsProgramViewAble = (string)value; break;
						case "IsProgramApprovalAble": this.str.IsProgramApprovalAble = (string)value; break;
						case "IsProgramUnApprovalAble": this.str.IsProgramUnApprovalAble = (string)value; break;
						case "IsProgramVoidAble": this.str.IsProgramVoidAble = (string)value; break;
						case "IsProgramUnVoidAble": this.str.IsProgramUnVoidAble = (string)value; break;
						case "IsProgramDirectVoid": this.str.IsProgramDirectVoid = (string)value; break;
						case "IsProgramPrintAble": this.str.IsProgramPrintAble = (string)value; break;
						case "IsMenuAddVisible": this.str.IsMenuAddVisible = (string)value; break;
						case "IsMenuHomeVisible": this.str.IsMenuHomeVisible = (string)value; break;
						case "IsVisible": this.str.IsVisible = (string)value; break;
						case "IsDiscontinue": this.str.IsDiscontinue = (string)value; break;
						case "NavigateUrl": this.str.NavigateUrl = (string)value; break;
						case "HelpLinkID": this.str.HelpLinkID = (string)value; break;
						case "AssemblyName": this.str.AssemblyName = (string)value; break;
						case "AssemblyClassName": this.str.AssemblyClassName = (string)value; break;
						case "StoreProcedureName": this.str.StoreProcedureName = (string)value; break;
						case "AccessKey": this.str.AccessKey = (string)value; break;
						case "IsUsingReportHeader": this.str.IsUsingReportHeader = (string)value; break;
						case "IsDirectPrintEnable": this.str.IsDirectPrintEnable = (string)value; break;
						case "IsListLoadRecordOnInit": this.str.IsListLoadRecordOnInit = (string)value; break;
						case "IsListLoadRecordIfFiltered": this.str.IsListLoadRecordIfFiltered = (string)value; break;
						case "IsProgramRedirected": this.str.IsProgramRedirected = (string)value; break;
						case "ApplicationID": this.str.ApplicationID = (string)value; break;
						case "ZplCommandTemplate": this.str.ZplCommandTemplate = (string)value; break;
						case "IsProgramExportAble": this.str.IsProgramExportAble = (string)value; break;
						case "IsProgramCrossUnitAble": this.str.IsProgramCrossUnitAble = (string)value; break;
						case "IsProgramPowerUserAble": this.str.IsProgramPowerUserAble = (string)value; break;
						case "SRProgramCategory": this.str.SRProgramCategory = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RootLevel":
						
							if (value == null || value is System.Byte)
								this.RootLevel = (System.Byte?)value;
							break;
						case "RowIndex":
						
							if (value == null || value is System.Int16)
								this.RowIndex = (System.Int16?)value;
							break;
						case "IsParentProgram":
						
							if (value == null || value is System.Boolean)
								this.IsParentProgram = (System.Boolean?)value;
							break;
						case "IsProgram":
						
							if (value == null || value is System.Boolean)
								this.IsProgram = (System.Boolean?)value;
							break;
						case "IsBeginGroup":
						
							if (value == null || value is System.Boolean)
								this.IsBeginGroup = (System.Boolean?)value;
							break;
						case "IsProgramAddAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramAddAble = (System.Boolean?)value;
							break;
						case "IsProgramEditAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramEditAble = (System.Boolean?)value;
							break;
						case "IsProgramDeleteAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramDeleteAble = (System.Boolean?)value;
							break;
						case "IsProgramViewAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramViewAble = (System.Boolean?)value;
							break;
						case "IsProgramApprovalAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramApprovalAble = (System.Boolean?)value;
							break;
						case "IsProgramUnApprovalAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramUnApprovalAble = (System.Boolean?)value;
							break;
						case "IsProgramVoidAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramVoidAble = (System.Boolean?)value;
							break;
						case "IsProgramUnVoidAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramUnVoidAble = (System.Boolean?)value;
							break;
						case "IsProgramDirectVoid":
						
							if (value == null || value is System.Boolean)
								this.IsProgramDirectVoid = (System.Boolean?)value;
							break;
						case "IsProgramPrintAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramPrintAble = (System.Boolean?)value;
							break;
						case "IsMenuAddVisible":
						
							if (value == null || value is System.Boolean)
								this.IsMenuAddVisible = (System.Boolean?)value;
							break;
						case "IsMenuHomeVisible":
						
							if (value == null || value is System.Boolean)
								this.IsMenuHomeVisible = (System.Boolean?)value;
							break;
						case "IsVisible":
						
							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
							break;
						case "IsDiscontinue":
						
							if (value == null || value is System.Boolean)
								this.IsDiscontinue = (System.Boolean?)value;
							break;
						case "IsUsingReportHeader":
						
							if (value == null || value is System.Boolean)
								this.IsUsingReportHeader = (System.Boolean?)value;
							break;
						case "IsDirectPrintEnable":
						
							if (value == null || value is System.Boolean)
								this.IsDirectPrintEnable = (System.Boolean?)value;
							break;
						case "IsListLoadRecordOnInit":
						
							if (value == null || value is System.Boolean)
								this.IsListLoadRecordOnInit = (System.Boolean?)value;
							break;
						case "IsListLoadRecordIfFiltered":
						
							if (value == null || value is System.Boolean)
								this.IsListLoadRecordIfFiltered = (System.Boolean?)value;
							break;
						case "IsProgramRedirected":
						
							if (value == null || value is System.Boolean)
								this.IsProgramRedirected = (System.Boolean?)value;
							break;
						case "IsProgramExportAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramExportAble = (System.Boolean?)value;
							break;
						case "IsProgramCrossUnitAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramCrossUnitAble = (System.Boolean?)value;
							break;
						case "IsProgramPowerUserAble":
						
							if (value == null || value is System.Boolean)
								this.IsProgramPowerUserAble = (System.Boolean?)value;
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
		/// Maps to AppProgram.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ProgramID);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.ParentProgramID
		/// </summary>
		virtual public System.String ParentProgramID
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ParentProgramID);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.ParentProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.ProgramName
		/// </summary>
		virtual public System.String ProgramName
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ProgramName);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.ProgramName, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.TopLevelProgramID
		/// </summary>
		virtual public System.String TopLevelProgramID
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.TopLevelProgramID);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.TopLevelProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.RootLevel
		/// </summary>
		virtual public System.Byte? RootLevel
		{
			get
			{
				return base.GetSystemByte(AppProgramMetadata.ColumnNames.RootLevel);
			}
			
			set
			{
				base.SetSystemByte(AppProgramMetadata.ColumnNames.RootLevel, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.RowIndex
		/// </summary>
		virtual public System.Int16? RowIndex
		{
			get
			{
				return base.GetSystemInt16(AppProgramMetadata.ColumnNames.RowIndex);
			}
			
			set
			{
				base.SetSystemInt16(AppProgramMetadata.ColumnNames.RowIndex, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsParentProgram
		/// </summary>
		virtual public System.Boolean? IsParentProgram
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsParentProgram);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsParentProgram, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgram
		/// </summary>
		virtual public System.Boolean? IsProgram
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgram);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgram, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsBeginGroup
		/// </summary>
		virtual public System.Boolean? IsBeginGroup
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsBeginGroup);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsBeginGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.ProgramType
		/// </summary>
		virtual public System.String ProgramType
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ProgramType);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.ProgramType, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramAddAble
		/// </summary>
		virtual public System.Boolean? IsProgramAddAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramAddAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramAddAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramEditAble
		/// </summary>
		virtual public System.Boolean? IsProgramEditAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramEditAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramEditAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramDeleteAble
		/// </summary>
		virtual public System.Boolean? IsProgramDeleteAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramDeleteAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramDeleteAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramViewAble
		/// </summary>
		virtual public System.Boolean? IsProgramViewAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramViewAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramViewAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramApprovalAble
		/// </summary>
		virtual public System.Boolean? IsProgramApprovalAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramApprovalAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramApprovalAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramUnApprovalAble
		/// </summary>
		virtual public System.Boolean? IsProgramUnApprovalAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramVoidAble
		/// </summary>
		virtual public System.Boolean? IsProgramVoidAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramVoidAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramVoidAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramUnVoidAble
		/// </summary>
		virtual public System.Boolean? IsProgramUnVoidAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramUnVoidAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramUnVoidAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramDirectVoid
		/// </summary>
		virtual public System.Boolean? IsProgramDirectVoid
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramDirectVoid);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramDirectVoid, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramPrintAble
		/// </summary>
		virtual public System.Boolean? IsProgramPrintAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramPrintAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramPrintAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsMenuAddVisible
		/// </summary>
		virtual public System.Boolean? IsMenuAddVisible
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsMenuAddVisible);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsMenuAddVisible, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsMenuHomeVisible
		/// </summary>
		virtual public System.Boolean? IsMenuHomeVisible
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsMenuHomeVisible);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsMenuHomeVisible, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsVisible);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsVisible, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsDiscontinue
		/// </summary>
		virtual public System.Boolean? IsDiscontinue
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsDiscontinue);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsDiscontinue, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.NavigateUrl
		/// </summary>
		virtual public System.String NavigateUrl
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.NavigateUrl);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.NavigateUrl, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.HelpLinkID
		/// </summary>
		virtual public System.String HelpLinkID
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.HelpLinkID);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.HelpLinkID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.AssemblyName
		/// </summary>
		virtual public System.String AssemblyName
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.AssemblyName);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.AssemblyName, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.AssemblyClassName
		/// </summary>
		virtual public System.String AssemblyClassName
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.AssemblyClassName);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.AssemblyClassName, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.StoreProcedureName
		/// </summary>
		virtual public System.String StoreProcedureName
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.StoreProcedureName);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.StoreProcedureName, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.AccessKey
		/// </summary>
		virtual public System.String AccessKey
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.AccessKey);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.AccessKey, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsUsingReportHeader
		/// </summary>
		virtual public System.Boolean? IsUsingReportHeader
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsUsingReportHeader);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsUsingReportHeader, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsDirectPrintEnable
		/// </summary>
		virtual public System.Boolean? IsDirectPrintEnable
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsDirectPrintEnable);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsDirectPrintEnable, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsListLoadRecordOnInit
		/// </summary>
		virtual public System.Boolean? IsListLoadRecordOnInit
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsListLoadRecordOnInit);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsListLoadRecordOnInit, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsListLoadRecordIfFiltered
		/// </summary>
		virtual public System.Boolean? IsListLoadRecordIfFiltered
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsListLoadRecordIfFiltered);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsListLoadRecordIfFiltered, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramRedirected
		/// </summary>
		virtual public System.Boolean? IsProgramRedirected
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramRedirected);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramRedirected, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.ApplicationID
		/// </summary>
		virtual public System.String ApplicationID
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ApplicationID);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.ApplicationID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.ZplCommandTemplate
		/// </summary>
		virtual public System.String ZplCommandTemplate
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ZplCommandTemplate);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.ZplCommandTemplate, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramExportAble
		/// </summary>
		virtual public System.Boolean? IsProgramExportAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramExportAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramExportAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramCrossUnitAble
		/// </summary>
		virtual public System.Boolean? IsProgramCrossUnitAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramCrossUnitAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramCrossUnitAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.IsProgramPowerUserAble
		/// </summary>
		virtual public System.Boolean? IsProgramPowerUserAble
		{
			get
			{
				return base.GetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramPowerUserAble);
			}
			
			set
			{
				base.SetSystemBoolean(AppProgramMetadata.ColumnNames.IsProgramPowerUserAble, value);
			}
		}
		/// <summary>
		/// Maps to AppProgram.SRProgramCategory
		/// </summary>
		virtual public System.String SRProgramCategory
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.SRProgramCategory);
			}
			
			set
			{
				base.SetSystemString(AppProgramMetadata.ColumnNames.SRProgramCategory, value);
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
			public esStrings(esAppProgram entity)
			{
				this.entity = entity;
			}
			public System.String ProgramID
			{
				get
				{
					System.String data = entity.ProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramID = null;
					else entity.ProgramID = Convert.ToString(value);
				}
			}
			public System.String ParentProgramID
			{
				get
				{
					System.String data = entity.ParentProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentProgramID = null;
					else entity.ParentProgramID = Convert.ToString(value);
				}
			}
			public System.String ProgramName
			{
				get
				{
					System.String data = entity.ProgramName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramName = null;
					else entity.ProgramName = Convert.ToString(value);
				}
			}
			public System.String TopLevelProgramID
			{
				get
				{
					System.String data = entity.TopLevelProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TopLevelProgramID = null;
					else entity.TopLevelProgramID = Convert.ToString(value);
				}
			}
			public System.String RootLevel
			{
				get
				{
					System.Byte? data = entity.RootLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RootLevel = null;
					else entity.RootLevel = Convert.ToByte(value);
				}
			}
			public System.String RowIndex
			{
				get
				{
					System.Int16? data = entity.RowIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RowIndex = null;
					else entity.RowIndex = Convert.ToInt16(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
				}
			}
			public System.String IsParentProgram
			{
				get
				{
					System.Boolean? data = entity.IsParentProgram;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParentProgram = null;
					else entity.IsParentProgram = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgram
			{
				get
				{
					System.Boolean? data = entity.IsProgram;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgram = null;
					else entity.IsProgram = Convert.ToBoolean(value);
				}
			}
			public System.String IsBeginGroup
			{
				get
				{
					System.Boolean? data = entity.IsBeginGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBeginGroup = null;
					else entity.IsBeginGroup = Convert.ToBoolean(value);
				}
			}
			public System.String ProgramType
			{
				get
				{
					System.String data = entity.ProgramType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramType = null;
					else entity.ProgramType = Convert.ToString(value);
				}
			}
			public System.String IsProgramAddAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramAddAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramAddAble = null;
					else entity.IsProgramAddAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramEditAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramEditAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramEditAble = null;
					else entity.IsProgramEditAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramDeleteAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramDeleteAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramDeleteAble = null;
					else entity.IsProgramDeleteAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramViewAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramViewAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramViewAble = null;
					else entity.IsProgramViewAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramApprovalAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramApprovalAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramApprovalAble = null;
					else entity.IsProgramApprovalAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramUnApprovalAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramUnApprovalAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramUnApprovalAble = null;
					else entity.IsProgramUnApprovalAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramVoidAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramVoidAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramVoidAble = null;
					else entity.IsProgramVoidAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramUnVoidAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramUnVoidAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramUnVoidAble = null;
					else entity.IsProgramUnVoidAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramDirectVoid
			{
				get
				{
					System.Boolean? data = entity.IsProgramDirectVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramDirectVoid = null;
					else entity.IsProgramDirectVoid = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramPrintAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramPrintAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramPrintAble = null;
					else entity.IsProgramPrintAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsMenuAddVisible
			{
				get
				{
					System.Boolean? data = entity.IsMenuAddVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMenuAddVisible = null;
					else entity.IsMenuAddVisible = Convert.ToBoolean(value);
				}
			}
			public System.String IsMenuHomeVisible
			{
				get
				{
					System.Boolean? data = entity.IsMenuHomeVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMenuHomeVisible = null;
					else entity.IsMenuHomeVisible = Convert.ToBoolean(value);
				}
			}
			public System.String IsVisible
			{
				get
				{
					System.Boolean? data = entity.IsVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisible = null;
					else entity.IsVisible = Convert.ToBoolean(value);
				}
			}
			public System.String IsDiscontinue
			{
				get
				{
					System.Boolean? data = entity.IsDiscontinue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscontinue = null;
					else entity.IsDiscontinue = Convert.ToBoolean(value);
				}
			}
			public System.String NavigateUrl
			{
				get
				{
					System.String data = entity.NavigateUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NavigateUrl = null;
					else entity.NavigateUrl = Convert.ToString(value);
				}
			}
			public System.String HelpLinkID
			{
				get
				{
					System.String data = entity.HelpLinkID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HelpLinkID = null;
					else entity.HelpLinkID = Convert.ToString(value);
				}
			}
			public System.String AssemblyName
			{
				get
				{
					System.String data = entity.AssemblyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssemblyName = null;
					else entity.AssemblyName = Convert.ToString(value);
				}
			}
			public System.String AssemblyClassName
			{
				get
				{
					System.String data = entity.AssemblyClassName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssemblyClassName = null;
					else entity.AssemblyClassName = Convert.ToString(value);
				}
			}
			public System.String StoreProcedureName
			{
				get
				{
					System.String data = entity.StoreProcedureName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StoreProcedureName = null;
					else entity.StoreProcedureName = Convert.ToString(value);
				}
			}
			public System.String AccessKey
			{
				get
				{
					System.String data = entity.AccessKey;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccessKey = null;
					else entity.AccessKey = Convert.ToString(value);
				}
			}
			public System.String IsUsingReportHeader
			{
				get
				{
					System.Boolean? data = entity.IsUsingReportHeader;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingReportHeader = null;
					else entity.IsUsingReportHeader = Convert.ToBoolean(value);
				}
			}
			public System.String IsDirectPrintEnable
			{
				get
				{
					System.Boolean? data = entity.IsDirectPrintEnable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirectPrintEnable = null;
					else entity.IsDirectPrintEnable = Convert.ToBoolean(value);
				}
			}
			public System.String IsListLoadRecordOnInit
			{
				get
				{
					System.Boolean? data = entity.IsListLoadRecordOnInit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsListLoadRecordOnInit = null;
					else entity.IsListLoadRecordOnInit = Convert.ToBoolean(value);
				}
			}
			public System.String IsListLoadRecordIfFiltered
			{
				get
				{
					System.Boolean? data = entity.IsListLoadRecordIfFiltered;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsListLoadRecordIfFiltered = null;
					else entity.IsListLoadRecordIfFiltered = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramRedirected
			{
				get
				{
					System.Boolean? data = entity.IsProgramRedirected;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramRedirected = null;
					else entity.IsProgramRedirected = Convert.ToBoolean(value);
				}
			}
			public System.String ApplicationID
			{
				get
				{
					System.String data = entity.ApplicationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicationID = null;
					else entity.ApplicationID = Convert.ToString(value);
				}
			}
			public System.String ZplCommandTemplate
			{
				get
				{
					System.String data = entity.ZplCommandTemplate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZplCommandTemplate = null;
					else entity.ZplCommandTemplate = Convert.ToString(value);
				}
			}
			public System.String IsProgramExportAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramExportAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramExportAble = null;
					else entity.IsProgramExportAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramCrossUnitAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramCrossUnitAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramCrossUnitAble = null;
					else entity.IsProgramCrossUnitAble = Convert.ToBoolean(value);
				}
			}
			public System.String IsProgramPowerUserAble
			{
				get
				{
					System.Boolean? data = entity.IsProgramPowerUserAble;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProgramPowerUserAble = null;
					else entity.IsProgramPowerUserAble = Convert.ToBoolean(value);
				}
			}
			public System.String SRProgramCategory
			{
				get
				{
					System.String data = entity.SRProgramCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProgramCategory = null;
					else entity.SRProgramCategory = Convert.ToString(value);
				}
			}
			private esAppProgram entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppProgramQuery query)
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
				throw new Exception("esAppProgram can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppProgram : esAppProgram
	{	
	}

	[Serializable]
	abstract public class esAppProgramQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramMetadata.Meta();
			}
		}	
			
		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParentProgramID
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.ParentProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem ProgramName
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.ProgramName, esSystemType.String);
			}
		} 
			
		public esQueryItem TopLevelProgramID
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.TopLevelProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem RootLevel
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.RootLevel, esSystemType.Byte);
			}
		} 
			
		public esQueryItem RowIndex
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.RowIndex, esSystemType.Int16);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem IsParentProgram
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsParentProgram, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgram
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgram, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsBeginGroup
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsBeginGroup, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ProgramType
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.ProgramType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsProgramAddAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramAddAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramEditAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramEditAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramDeleteAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramDeleteAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramViewAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramViewAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramApprovalAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramApprovalAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramUnApprovalAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramVoidAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramVoidAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramUnVoidAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramUnVoidAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramDirectVoid
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramDirectVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramPrintAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramPrintAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsMenuAddVisible
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsMenuAddVisible, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsMenuHomeVisible
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsMenuHomeVisible, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDiscontinue
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsDiscontinue, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem NavigateUrl
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.NavigateUrl, esSystemType.String);
			}
		} 
			
		public esQueryItem HelpLinkID
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.HelpLinkID, esSystemType.String);
			}
		} 
			
		public esQueryItem AssemblyName
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.AssemblyName, esSystemType.String);
			}
		} 
			
		public esQueryItem AssemblyClassName
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.AssemblyClassName, esSystemType.String);
			}
		} 
			
		public esQueryItem StoreProcedureName
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.StoreProcedureName, esSystemType.String);
			}
		} 
			
		public esQueryItem AccessKey
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.AccessKey, esSystemType.String);
			}
		} 
			
		public esQueryItem IsUsingReportHeader
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsUsingReportHeader, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDirectPrintEnable
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsDirectPrintEnable, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsListLoadRecordOnInit
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsListLoadRecordOnInit, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsListLoadRecordIfFiltered
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsListLoadRecordIfFiltered, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramRedirected
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramRedirected, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApplicationID
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.ApplicationID, esSystemType.String);
			}
		} 
			
		public esQueryItem ZplCommandTemplate
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.ZplCommandTemplate, esSystemType.String);
			}
		} 
			
		public esQueryItem IsProgramExportAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramExportAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramCrossUnitAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramCrossUnitAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsProgramPowerUserAble
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramPowerUserAble, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRProgramCategory
		{
			get
			{
				return new esQueryItem(this, AppProgramMetadata.ColumnNames.SRProgramCategory, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppProgramCollection")]
	public partial class AppProgramCollection : esAppProgramCollection, IEnumerable< AppProgram>
	{
		public AppProgramCollection()
		{

		}	
		
		public static implicit operator List< AppProgram>(AppProgramCollection coll)
		{
			List< AppProgram> list = new List< AppProgram>();
			
			foreach (AppProgram emp in coll)
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
				return  AppProgramMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppProgram(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppProgram();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppProgramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramQuery();
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
		public bool Load(AppProgramQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppProgram AddNew()
		{
			AppProgram entity = base.AddNewEntity() as AppProgram;
			
			return entity;		
		}
		public AppProgram FindByPrimaryKey(String programID)
		{
			return base.FindByPrimaryKey(programID) as AppProgram;
		}

		#region IEnumerable< AppProgram> Members

		IEnumerator< AppProgram> IEnumerable< AppProgram>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppProgram;
			}
		}

		#endregion
		
		private AppProgramQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppProgram' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppProgram ({ProgramID})")]
	[Serializable]
	public partial class AppProgram : esAppProgram
	{
		public AppProgram()
		{
		}	
	
		public AppProgram(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramMetadata.Meta();
			}
		}	
	
		override protected esAppProgramQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppProgramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramQuery();
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
		public bool Load(AppProgramQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppProgramQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppProgramQuery : esAppProgramQuery
	{
		public AppProgramQuery()
		{

		}		
		
		public AppProgramQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppProgramQuery";
        }
	}

	[Serializable]
	public partial class AppProgramMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppProgramMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ParentProgramID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ParentProgramID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ProgramName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ProgramName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.TopLevelProgramID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.TopLevelProgramID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.RootLevel, 4, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = AppProgramMetadata.PropertyNames.RootLevel;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.RowIndex, 5, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = AppProgramMetadata.PropertyNames.RowIndex;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.Note, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsParentProgram, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsParentProgram;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgram, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgram;
			c.HasDefault = true;
			c.Default = @"((1))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsBeginGroup, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsBeginGroup;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ProgramType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ProgramType;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramAddAble, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramAddAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramEditAble, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramEditAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramDeleteAble, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramDeleteAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramViewAble, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramViewAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramApprovalAble, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramApprovalAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramUnApprovalAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramVoidAble, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramVoidAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramUnVoidAble, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramUnVoidAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramDirectVoid, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramDirectVoid;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramPrintAble, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramPrintAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsMenuAddVisible, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsMenuAddVisible;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsMenuHomeVisible, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsMenuHomeVisible;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsVisible, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsVisible;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsDiscontinue, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsDiscontinue;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.NavigateUrl, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.NavigateUrl;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.HelpLinkID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.HelpLinkID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.AssemblyName, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.AssemblyName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.AssemblyClassName, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.AssemblyClassName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.StoreProcedureName, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.StoreProcedureName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.AccessKey, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.AccessKey;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsUsingReportHeader, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsUsingReportHeader;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsDirectPrintEnable, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsDirectPrintEnable;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsListLoadRecordOnInit, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsListLoadRecordOnInit;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsListLoadRecordIfFiltered, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsListLoadRecordIfFiltered;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramRedirected, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramRedirected;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ApplicationID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ApplicationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ZplCommandTemplate, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ZplCommandTemplate;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramExportAble, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramExportAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramCrossUnitAble, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramCrossUnitAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramPowerUserAble, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramPowerUserAble;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.SRProgramCategory, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.SRProgramCategory;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppProgramMetadata Meta()
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
			public const string ProgramID = "ProgramID";
			public const string ParentProgramID = "ParentProgramID";
			public const string ProgramName = "ProgramName";
			public const string TopLevelProgramID = "TopLevelProgramID";
			public const string RootLevel = "RootLevel";
			public const string RowIndex = "RowIndex";
			public const string Note = "Note";
			public const string IsParentProgram = "IsParentProgram";
			public const string IsProgram = "IsProgram";
			public const string IsBeginGroup = "IsBeginGroup";
			public const string ProgramType = "ProgramType";
			public const string IsProgramAddAble = "IsProgramAddAble";
			public const string IsProgramEditAble = "IsProgramEditAble";
			public const string IsProgramDeleteAble = "IsProgramDeleteAble";
			public const string IsProgramViewAble = "IsProgramViewAble";
			public const string IsProgramApprovalAble = "IsProgramApprovalAble";
			public const string IsProgramUnApprovalAble = "IsProgramUnApprovalAble";
			public const string IsProgramVoidAble = "IsProgramVoidAble";
			public const string IsProgramUnVoidAble = "IsProgramUnVoidAble";
			public const string IsProgramDirectVoid = "IsProgramDirectVoid";
			public const string IsProgramPrintAble = "IsProgramPrintAble";
			public const string IsMenuAddVisible = "IsMenuAddVisible";
			public const string IsMenuHomeVisible = "IsMenuHomeVisible";
			public const string IsVisible = "IsVisible";
			public const string IsDiscontinue = "IsDiscontinue";
			public const string NavigateUrl = "NavigateUrl";
			public const string HelpLinkID = "HelpLinkID";
			public const string AssemblyName = "AssemblyName";
			public const string AssemblyClassName = "AssemblyClassName";
			public const string StoreProcedureName = "StoreProcedureName";
			public const string AccessKey = "AccessKey";
			public const string IsUsingReportHeader = "IsUsingReportHeader";
			public const string IsDirectPrintEnable = "IsDirectPrintEnable";
			public const string IsListLoadRecordOnInit = "IsListLoadRecordOnInit";
			public const string IsListLoadRecordIfFiltered = "IsListLoadRecordIfFiltered";
			public const string IsProgramRedirected = "IsProgramRedirected";
			public const string ApplicationID = "ApplicationID";
			public const string ZplCommandTemplate = "ZplCommandTemplate";
			public const string IsProgramExportAble = "IsProgramExportAble";
			public const string IsProgramCrossUnitAble = "IsProgramCrossUnitAble";
			public const string IsProgramPowerUserAble = "IsProgramPowerUserAble";
			public const string SRProgramCategory = "SRProgramCategory";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ProgramID = "ProgramID";
			public const string ParentProgramID = "ParentProgramID";
			public const string ProgramName = "ProgramName";
			public const string TopLevelProgramID = "TopLevelProgramID";
			public const string RootLevel = "RootLevel";
			public const string RowIndex = "RowIndex";
			public const string Note = "Note";
			public const string IsParentProgram = "IsParentProgram";
			public const string IsProgram = "IsProgram";
			public const string IsBeginGroup = "IsBeginGroup";
			public const string ProgramType = "ProgramType";
			public const string IsProgramAddAble = "IsProgramAddAble";
			public const string IsProgramEditAble = "IsProgramEditAble";
			public const string IsProgramDeleteAble = "IsProgramDeleteAble";
			public const string IsProgramViewAble = "IsProgramViewAble";
			public const string IsProgramApprovalAble = "IsProgramApprovalAble";
			public const string IsProgramUnApprovalAble = "IsProgramUnApprovalAble";
			public const string IsProgramVoidAble = "IsProgramVoidAble";
			public const string IsProgramUnVoidAble = "IsProgramUnVoidAble";
			public const string IsProgramDirectVoid = "IsProgramDirectVoid";
			public const string IsProgramPrintAble = "IsProgramPrintAble";
			public const string IsMenuAddVisible = "IsMenuAddVisible";
			public const string IsMenuHomeVisible = "IsMenuHomeVisible";
			public const string IsVisible = "IsVisible";
			public const string IsDiscontinue = "IsDiscontinue";
			public const string NavigateUrl = "NavigateUrl";
			public const string HelpLinkID = "HelpLinkID";
			public const string AssemblyName = "AssemblyName";
			public const string AssemblyClassName = "AssemblyClassName";
			public const string StoreProcedureName = "StoreProcedureName";
			public const string AccessKey = "AccessKey";
			public const string IsUsingReportHeader = "IsUsingReportHeader";
			public const string IsDirectPrintEnable = "IsDirectPrintEnable";
			public const string IsListLoadRecordOnInit = "IsListLoadRecordOnInit";
			public const string IsListLoadRecordIfFiltered = "IsListLoadRecordIfFiltered";
			public const string IsProgramRedirected = "IsProgramRedirected";
			public const string ApplicationID = "ApplicationID";
			public const string ZplCommandTemplate = "ZplCommandTemplate";
			public const string IsProgramExportAble = "IsProgramExportAble";
			public const string IsProgramCrossUnitAble = "IsProgramCrossUnitAble";
			public const string IsProgramPowerUserAble = "IsProgramPowerUserAble";
			public const string SRProgramCategory = "SRProgramCategory";
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
			lock (typeof(AppProgramMetadata))
			{
				if(AppProgramMetadata.mapDelegates == null)
				{
					AppProgramMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppProgramMetadata.meta == null)
				{
					AppProgramMetadata.meta = new AppProgramMetadata();
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
				
				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProgramName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TopLevelProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RootLevel", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("RowIndex", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParentProgram", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgram", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBeginGroup", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ProgramType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProgramAddAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramEditAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramDeleteAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramViewAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramApprovalAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramUnApprovalAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramVoidAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramUnVoidAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramDirectVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramPrintAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMenuAddVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMenuHomeVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDiscontinue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NavigateUrl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HelpLinkID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssemblyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssemblyClassName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StoreProcedureName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccessKey", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsUsingReportHeader", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDirectPrintEnable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsListLoadRecordOnInit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsListLoadRecordIfFiltered", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramRedirected", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApplicationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZplCommandTemplate", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProgramExportAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramCrossUnitAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProgramPowerUserAble", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRProgramCategory", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AppProgram";
				meta.Destination = "AppProgram";
				meta.spInsert = "proc_AppProgramInsert";				
				meta.spUpdate = "proc_AppProgramUpdate";		
				meta.spDelete = "proc_AppProgramDelete";
				meta.spLoadAll = "proc_AppProgramLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppProgramLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppProgramMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
