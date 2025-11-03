/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/24/2021 11:35:42 PM
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
	abstract public class esLaunderedProcessCollection : esEntityCollectionWAuditLog
	{
		public esLaunderedProcessCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "LaunderedProcessCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esLaunderedProcessQuery query)
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
			this.InitQuery(query as esLaunderedProcessQuery);
		}
		#endregion
			
		virtual public LaunderedProcess DetachEntity(LaunderedProcess entity)
		{
			return base.DetachEntity(entity) as LaunderedProcess;
		}
		
		virtual public LaunderedProcess AttachEntity(LaunderedProcess entity)
		{
			return base.AttachEntity(entity) as LaunderedProcess;
		}
		
		virtual public void Combine(LaunderedProcessCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LaunderedProcess this[int index]
		{
			get
			{
				return base[index] as LaunderedProcess;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaunderedProcess);
		}
	}

	[Serializable]
	abstract public class esLaunderedProcess : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaunderedProcessQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esLaunderedProcess()
		{
		}
	
		public esLaunderedProcess(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String processNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String processNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String processNo)
		{
			esLaunderedProcessQuery query = this.GetDynamicQuery();
			query.Where(query.ProcessNo == processNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String processNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ProcessNo",processNo);
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
						case "ProcessNo": this.str.ProcessNo = (string)value; break;
						case "ProcessDate": this.str.ProcessDate = (string)value; break;
						case "ProcessTime": this.str.ProcessTime = (string)value; break;
						case "SRLaundryProcessType": this.str.SRLaundryProcessType = (string)value; break;
						case "SRLaundryProgram": this.str.SRLaundryProgram = (string)value; break;
						case "SRLaundryType": this.str.SRLaundryType = (string)value; break;
						case "MachineID": this.str.MachineID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ProcessDate":
						
							if (value == null || value is System.DateTime)
								this.ProcessDate = (System.DateTime?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":
						
							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
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
		/// Maps to LaunderedProcess.ProcessNo
		/// </summary>
		virtual public System.String ProcessNo
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.ProcessNo);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.ProcessNo, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.ProcessDate
		/// </summary>
		virtual public System.DateTime? ProcessDate
		{
			get
			{
				return base.GetSystemDateTime(LaunderedProcessMetadata.ColumnNames.ProcessDate);
			}
			
			set
			{
				base.SetSystemDateTime(LaunderedProcessMetadata.ColumnNames.ProcessDate, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.ProcessTime
		/// </summary>
		virtual public System.String ProcessTime
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.ProcessTime);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.ProcessTime, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.SRLaundryProcessType
		/// </summary>
		virtual public System.String SRLaundryProcessType
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.SRLaundryProcessType);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.SRLaundryProcessType, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.SRLaundryProgram
		/// </summary>
		virtual public System.String SRLaundryProgram
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.SRLaundryProgram);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.SRLaundryProgram, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.SRLaundryType
		/// </summary>
		virtual public System.String SRLaundryType
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.SRLaundryType);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.SRLaundryType, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.MachineID
		/// </summary>
		virtual public System.String MachineID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.MachineID);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.MachineID, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(LaunderedProcessMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(LaunderedProcessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaunderedProcessMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LaunderedProcessMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(LaunderedProcessMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(LaunderedProcessMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaunderedProcessMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LaunderedProcessMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaunderedProcessMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LaunderedProcessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcess.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LaunderedProcessMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaunderedProcess entity)
			{
				this.entity = entity;
			}
			public System.String ProcessNo
			{
				get
				{
					System.String data = entity.ProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessNo = null;
					else entity.ProcessNo = Convert.ToString(value);
				}
			}
			public System.String ProcessDate
			{
				get
				{
					System.DateTime? data = entity.ProcessDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessDate = null;
					else entity.ProcessDate = Convert.ToDateTime(value);
				}
			}
			public System.String ProcessTime
			{
				get
				{
					System.String data = entity.ProcessTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessTime = null;
					else entity.ProcessTime = Convert.ToString(value);
				}
			}
			public System.String SRLaundryProcessType
			{
				get
				{
					System.String data = entity.SRLaundryProcessType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryProcessType = null;
					else entity.SRLaundryProcessType = Convert.ToString(value);
				}
			}
			public System.String SRLaundryProgram
			{
				get
				{
					System.String data = entity.SRLaundryProgram;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryProgram = null;
					else entity.SRLaundryProgram = Convert.ToString(value);
				}
			}
			public System.String SRLaundryType
			{
				get
				{
					System.String data = entity.SRLaundryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryType = null;
					else entity.SRLaundryType = Convert.ToString(value);
				}
			}
			public System.String MachineID
			{
				get
				{
					System.String data = entity.MachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MachineID = null;
					else entity.MachineID = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			private esLaunderedProcess entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaunderedProcessQuery query)
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
				throw new Exception("esLaunderedProcess can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaunderedProcess : esLaunderedProcess
	{	
	}

	[Serializable]
	abstract public class esLaunderedProcessQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return LaunderedProcessMetadata.Meta();
			}
		}	
			
		public esQueryItem ProcessNo
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.ProcessNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ProcessDate
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.ProcessDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ProcessTime
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.ProcessTime, esSystemType.String);
			}
		} 
			
		public esQueryItem SRLaundryProcessType
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.SRLaundryProcessType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRLaundryProgram
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.SRLaundryProgram, esSystemType.String);
			}
		} 
			
		public esQueryItem SRLaundryType
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.SRLaundryType, esSystemType.String);
			}
		} 
			
		public esQueryItem MachineID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.MachineID, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaunderedProcessCollection")]
	public partial class LaunderedProcessCollection : esLaunderedProcessCollection, IEnumerable< LaunderedProcess>
	{
		public LaunderedProcessCollection()
		{

		}	
		
		public static implicit operator List< LaunderedProcess>(LaunderedProcessCollection coll)
		{
			List< LaunderedProcess> list = new List< LaunderedProcess>();
			
			foreach (LaunderedProcess emp in coll)
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
				return  LaunderedProcessMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaunderedProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaunderedProcess(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaunderedProcess();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public LaunderedProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaunderedProcessQuery();
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
		public bool Load(LaunderedProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaunderedProcess AddNew()
		{
			LaunderedProcess entity = base.AddNewEntity() as LaunderedProcess;
			
			return entity;		
		}
		public LaunderedProcess FindByPrimaryKey(String processNo)
		{
			return base.FindByPrimaryKey(processNo) as LaunderedProcess;
		}

		#region IEnumerable< LaunderedProcess> Members

		IEnumerator< LaunderedProcess> IEnumerable< LaunderedProcess>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LaunderedProcess;
			}
		}

		#endregion
		
		private LaunderedProcessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaunderedProcess' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaunderedProcess ({ProcessNo})")]
	[Serializable]
	public partial class LaunderedProcess : esLaunderedProcess
	{
		public LaunderedProcess()
		{
		}	
	
		public LaunderedProcess(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaunderedProcessMetadata.Meta();
			}
		}	
	
		override protected esLaunderedProcessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaunderedProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public LaunderedProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaunderedProcessQuery();
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
		public bool Load(LaunderedProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private LaunderedProcessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaunderedProcessQuery : esLaunderedProcessQuery
	{
		public LaunderedProcessQuery()
		{

		}		
		
		public LaunderedProcessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "LaunderedProcessQuery";
        }
	}

	[Serializable]
	public partial class LaunderedProcessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaunderedProcessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.ProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.ProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.ProcessDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.ProcessDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.ProcessTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.ProcessTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.SRLaundryProcessType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.SRLaundryProcessType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.SRLaundryProgram, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.SRLaundryProgram;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.SRLaundryType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.SRLaundryType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.MachineID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.MachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.ApprovedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.VoidDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.VoidByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaunderedProcessMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public LaunderedProcessMetadata Meta()
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
			public const string ProcessNo = "ProcessNo";
			public const string ProcessDate = "ProcessDate";
			public const string ProcessTime = "ProcessTime";
			public const string SRLaundryProcessType = "SRLaundryProcessType";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string SRLaundryType = "SRLaundryType";
			public const string MachineID = "MachineID";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ProcessNo = "ProcessNo";
			public const string ProcessDate = "ProcessDate";
			public const string ProcessTime = "ProcessTime";
			public const string SRLaundryProcessType = "SRLaundryProcessType";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string SRLaundryType = "SRLaundryType";
			public const string MachineID = "MachineID";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(LaunderedProcessMetadata))
			{
				if(LaunderedProcessMetadata.mapDelegates == null)
				{
					LaunderedProcessMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LaunderedProcessMetadata.meta == null)
				{
					LaunderedProcessMetadata.meta = new LaunderedProcessMetadata();
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
				
				meta.AddTypeMap("ProcessNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ProcessTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryProcessType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryProgram", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "LaunderedProcess";
				meta.Destination = "LaunderedProcess";
				meta.spInsert = "proc_LaunderedProcessInsert";				
				meta.spUpdate = "proc_LaunderedProcessUpdate";		
				meta.spDelete = "proc_LaunderedProcessDelete";
				meta.spLoadAll = "proc_LaunderedProcessLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaunderedProcessLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaunderedProcessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
