/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/19/2022 6:56:30 PM
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
	abstract public class esConsumeMethodCollection : esEntityCollectionWAuditLog
	{
		public esConsumeMethodCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ConsumeMethodCollection";
		}

		#region Query Logic
		protected void InitQuery(esConsumeMethodQuery query)
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
			this.InitQuery(query as esConsumeMethodQuery);
		}
		#endregion

		virtual public ConsumeMethod DetachEntity(ConsumeMethod entity)
		{
			return base.DetachEntity(entity) as ConsumeMethod;
		}

		virtual public ConsumeMethod AttachEntity(ConsumeMethod entity)
		{
			return base.AttachEntity(entity) as ConsumeMethod;
		}

		virtual public void Combine(ConsumeMethodCollection collection)
		{
			base.Combine(collection);
		}

		new public ConsumeMethod this[int index]
		{
			get
			{
				return base[index] as ConsumeMethod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ConsumeMethod);
		}
	}

	[Serializable]
	abstract public class esConsumeMethod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esConsumeMethodQuery GetDynamicQuery()
		{
			return null;
		}

		public esConsumeMethod()
		{
		}

		public esConsumeMethod(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRConsumeMethod)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRConsumeMethod);
			else
				return LoadByPrimaryKeyStoredProcedure(sRConsumeMethod);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRConsumeMethod)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRConsumeMethod);
			else
				return LoadByPrimaryKeyStoredProcedure(sRConsumeMethod);
		}

		private bool LoadByPrimaryKeyDynamic(String sRConsumeMethod)
		{
			esConsumeMethodQuery query = this.GetDynamicQuery();
			query.Where(query.SRConsumeMethod == sRConsumeMethod);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRConsumeMethod)
		{
			esParameters parms = new esParameters();
			parms.Add("SRConsumeMethod", sRConsumeMethod);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "SRConsumeMethodName": this.str.SRConsumeMethodName = (string)value; break;
						case "TimeSequence": this.str.TimeSequence = (string)value; break;
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
						case "LastCreateByUserID": this.str.LastCreateByUserID = (string)value; break;
						case "SygnaText": this.str.SygnaText = (string)value; break;
						case "IterationQty": this.str.IterationQty = (string)value; break;
						case "IterationInInterval": this.str.IterationInInterval = (string)value; break;
						case "Time01": this.str.Time01 = (string)value; break;
						case "Time02": this.str.Time02 = (string)value; break;
						case "Time03": this.str.Time03 = (string)value; break;
						case "Time04": this.str.Time04 = (string)value; break;
						case "Time05": this.str.Time05 = (string)value; break;
						case "Time06": this.str.Time06 = (string)value; break;
						case "Time07": this.str.Time07 = (string)value; break;
						case "Time08": this.str.Time08 = (string)value; break;
						case "Time09": this.str.Time09 = (string)value; break;
						case "Time10": this.str.Time10 = (string)value; break;
						case "LineNumber": this.str.LineNumber = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LastCreateDateTime":

							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
							break;
						case "IterationQty":

							if (value == null || value is System.Int32)
								this.IterationQty = (System.Int32?)value;
							break;
						case "LineNumber":

							if (value == null || value is System.Int32)
								this.LineNumber = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to ConsumeMethod.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.SRConsumeMethodName
		/// </summary>
		virtual public System.String SRConsumeMethodName
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.SRConsumeMethodName);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.SRConsumeMethodName, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.TimeSequence
		/// </summary>
		virtual public System.String TimeSequence
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.TimeSequence);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.TimeSequence, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ConsumeMethodMetadata.ColumnNames.LastCreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ConsumeMethodMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.LastCreateByUserID
		/// </summary>
		virtual public System.String LastCreateByUserID
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.LastCreateByUserID);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.LastCreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.SygnaText
		/// </summary>
		virtual public System.String SygnaText
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.SygnaText);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.SygnaText, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.IterationQty
		/// </summary>
		virtual public System.Int32? IterationQty
		{
			get
			{
				return base.GetSystemInt32(ConsumeMethodMetadata.ColumnNames.IterationQty);
			}

			set
			{
				base.SetSystemInt32(ConsumeMethodMetadata.ColumnNames.IterationQty, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.IterationInInterval
		/// </summary>
		virtual public System.String IterationInInterval
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.IterationInInterval);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.IterationInInterval, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time01
		/// </summary>
		virtual public System.String Time01
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time01);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time01, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time02
		/// </summary>
		virtual public System.String Time02
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time02);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time02, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time03
		/// </summary>
		virtual public System.String Time03
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time03);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time03, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time04
		/// </summary>
		virtual public System.String Time04
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time04);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time04, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time05
		/// </summary>
		virtual public System.String Time05
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time05);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time05, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time06
		/// </summary>
		virtual public System.String Time06
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time06);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time06, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time07
		/// </summary>
		virtual public System.String Time07
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time07);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time07, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time08
		/// </summary>
		virtual public System.String Time08
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time08);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time08, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time09
		/// </summary>
		virtual public System.String Time09
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time09);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time09, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.Time10
		/// </summary>
		virtual public System.String Time10
		{
			get
			{
				return base.GetSystemString(ConsumeMethodMetadata.ColumnNames.Time10);
			}

			set
			{
				base.SetSystemString(ConsumeMethodMetadata.ColumnNames.Time10, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.LineNumber
		/// </summary>
		virtual public System.Int32? LineNumber
		{
			get
			{
				return base.GetSystemInt32(ConsumeMethodMetadata.ColumnNames.LineNumber);
			}

			set
			{
				base.SetSystemInt32(ConsumeMethodMetadata.ColumnNames.LineNumber, value);
			}
		}
		/// <summary>
		/// Maps to ConsumeMethod.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ConsumeMethodMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(ConsumeMethodMetadata.ColumnNames.IsActive, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esConsumeMethod entity)
			{
				this.entity = entity;
			}
			public System.String SRConsumeMethod
			{
				get
				{
					System.String data = entity.SRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
					else entity.SRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String SRConsumeMethodName
			{
				get
				{
					System.String data = entity.SRConsumeMethodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethodName = null;
					else entity.SRConsumeMethodName = Convert.ToString(value);
				}
			}
			public System.String TimeSequence
			{
				get
				{
					System.String data = entity.TimeSequence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TimeSequence = null;
					else entity.TimeSequence = Convert.ToString(value);
				}
			}
			public System.String LastCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
					else entity.LastCreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCreateByUserID
			{
				get
				{
					System.String data = entity.LastCreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateByUserID = null;
					else entity.LastCreateByUserID = Convert.ToString(value);
				}
			}
			public System.String SygnaText
			{
				get
				{
					System.String data = entity.SygnaText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SygnaText = null;
					else entity.SygnaText = Convert.ToString(value);
				}
			}
			public System.String IterationQty
			{
				get
				{
					System.Int32? data = entity.IterationQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IterationQty = null;
					else entity.IterationQty = Convert.ToInt32(value);
				}
			}
			public System.String IterationInInterval
			{
				get
				{
					System.String data = entity.IterationInInterval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IterationInInterval = null;
					else entity.IterationInInterval = Convert.ToString(value);
				}
			}
			public System.String Time01
			{
				get
				{
					System.String data = entity.Time01;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time01 = null;
					else entity.Time01 = Convert.ToString(value);
				}
			}
			public System.String Time02
			{
				get
				{
					System.String data = entity.Time02;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time02 = null;
					else entity.Time02 = Convert.ToString(value);
				}
			}
			public System.String Time03
			{
				get
				{
					System.String data = entity.Time03;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time03 = null;
					else entity.Time03 = Convert.ToString(value);
				}
			}
			public System.String Time04
			{
				get
				{
					System.String data = entity.Time04;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time04 = null;
					else entity.Time04 = Convert.ToString(value);
				}
			}
			public System.String Time05
			{
				get
				{
					System.String data = entity.Time05;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time05 = null;
					else entity.Time05 = Convert.ToString(value);
				}
			}
			public System.String Time06
			{
				get
				{
					System.String data = entity.Time06;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time06 = null;
					else entity.Time06 = Convert.ToString(value);
				}
			}
			public System.String Time07
			{
				get
				{
					System.String data = entity.Time07;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time07 = null;
					else entity.Time07 = Convert.ToString(value);
				}
			}
			public System.String Time08
			{
				get
				{
					System.String data = entity.Time08;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time08 = null;
					else entity.Time08 = Convert.ToString(value);
				}
			}
			public System.String Time09
			{
				get
				{
					System.String data = entity.Time09;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time09 = null;
					else entity.Time09 = Convert.ToString(value);
				}
			}
			public System.String Time10
			{
				get
				{
					System.String data = entity.Time10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time10 = null;
					else entity.Time10 = Convert.ToString(value);
				}
			}
			public System.String LineNumber
			{
				get
				{
					System.Int32? data = entity.LineNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LineNumber = null;
					else entity.LineNumber = Convert.ToInt32(value);
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
			private esConsumeMethod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esConsumeMethodQuery query)
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
				throw new Exception("esConsumeMethod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ConsumeMethod : esConsumeMethod
	{
	}

	[Serializable]
	abstract public class esConsumeMethodQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ConsumeMethodMetadata.Meta();
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeMethodName
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.SRConsumeMethodName, esSystemType.String);
			}
		}

		public esQueryItem TimeSequence
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.TimeSequence, esSystemType.String);
			}
		}

		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCreateByUserID
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.LastCreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SygnaText
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.SygnaText, esSystemType.String);
			}
		}

		public esQueryItem IterationQty
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.IterationQty, esSystemType.Int32);
			}
		}

		public esQueryItem IterationInInterval
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.IterationInInterval, esSystemType.String);
			}
		}

		public esQueryItem Time01
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time01, esSystemType.String);
			}
		}

		public esQueryItem Time02
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time02, esSystemType.String);
			}
		}

		public esQueryItem Time03
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time03, esSystemType.String);
			}
		}

		public esQueryItem Time04
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time04, esSystemType.String);
			}
		}

		public esQueryItem Time05
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time05, esSystemType.String);
			}
		}

		public esQueryItem Time06
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time06, esSystemType.String);
			}
		}

		public esQueryItem Time07
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time07, esSystemType.String);
			}
		}

		public esQueryItem Time08
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time08, esSystemType.String);
			}
		}

		public esQueryItem Time09
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time09, esSystemType.String);
			}
		}

		public esQueryItem Time10
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.Time10, esSystemType.String);
			}
		}

		public esQueryItem LineNumber
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.LineNumber, esSystemType.Int32);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ConsumeMethodMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ConsumeMethodCollection")]
	public partial class ConsumeMethodCollection : esConsumeMethodCollection, IEnumerable<ConsumeMethod>
	{
		public ConsumeMethodCollection()
		{

		}

		public static implicit operator List<ConsumeMethod>(ConsumeMethodCollection coll)
		{
			List<ConsumeMethod> list = new List<ConsumeMethod>();

			foreach (ConsumeMethod emp in coll)
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
				return ConsumeMethodMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ConsumeMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ConsumeMethod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ConsumeMethod();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ConsumeMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ConsumeMethodQuery();
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
		public bool Load(ConsumeMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ConsumeMethod AddNew()
		{
			ConsumeMethod entity = base.AddNewEntity() as ConsumeMethod;

			return entity;
		}
		public ConsumeMethod FindByPrimaryKey(String sRConsumeMethod)
		{
			return base.FindByPrimaryKey(sRConsumeMethod) as ConsumeMethod;
		}

		#region IEnumerable< ConsumeMethod> Members

		IEnumerator<ConsumeMethod> IEnumerable<ConsumeMethod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ConsumeMethod;
			}
		}

		#endregion

		private ConsumeMethodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ConsumeMethod' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ConsumeMethod ({SRConsumeMethod})")]
	[Serializable]
	public partial class ConsumeMethod : esConsumeMethod
	{
		public ConsumeMethod()
		{
		}

		public ConsumeMethod(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ConsumeMethodMetadata.Meta();
			}
		}

		override protected esConsumeMethodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ConsumeMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ConsumeMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ConsumeMethodQuery();
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
		public bool Load(ConsumeMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ConsumeMethodQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ConsumeMethodQuery : esConsumeMethodQuery
	{
		public ConsumeMethodQuery()
		{

		}

		public ConsumeMethodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ConsumeMethodQuery";
		}
	}

	[Serializable]
	public partial class ConsumeMethodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ConsumeMethodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.SRConsumeMethod, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.SRConsumeMethod;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.SRConsumeMethodName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.SRConsumeMethodName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.TimeSequence, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.TimeSequence;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.LastCreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.LastCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.LastCreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.LastCreateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.SygnaText, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.SygnaText;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.IterationQty, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.IterationQty;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.IterationInInterval, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.IterationInInterval;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time01, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time01;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time02, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time02;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time03, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time03;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time04, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time04;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time05, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time05;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time06, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time06;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time07, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time07;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time08, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time08;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time09, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time09;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.Time10, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.Time10;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.LineNumber, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.LineNumber;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ConsumeMethodMetadata.ColumnNames.IsActive, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ConsumeMethodMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ConsumeMethodMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string SRConsumeMethodName = "SRConsumeMethodName";
			public const string TimeSequence = "TimeSequence";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateByUserID = "LastCreateByUserID";
			public const string SygnaText = "SygnaText";
			public const string IterationQty = "IterationQty";
			public const string IterationInInterval = "IterationInInterval";
			public const string Time01 = "Time01";
			public const string Time02 = "Time02";
			public const string Time03 = "Time03";
			public const string Time04 = "Time04";
			public const string Time05 = "Time05";
			public const string Time06 = "Time06";
			public const string Time07 = "Time07";
			public const string Time08 = "Time08";
			public const string Time09 = "Time09";
			public const string Time10 = "Time10";
			public const string LineNumber = "LineNumber";
			public const string IsActive = "IsActive";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string SRConsumeMethodName = "SRConsumeMethodName";
			public const string TimeSequence = "TimeSequence";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateByUserID = "LastCreateByUserID";
			public const string SygnaText = "SygnaText";
			public const string IterationQty = "IterationQty";
			public const string IterationInInterval = "IterationInInterval";
			public const string Time01 = "Time01";
			public const string Time02 = "Time02";
			public const string Time03 = "Time03";
			public const string Time04 = "Time04";
			public const string Time05 = "Time05";
			public const string Time06 = "Time06";
			public const string Time07 = "Time07";
			public const string Time08 = "Time08";
			public const string Time09 = "Time09";
			public const string Time10 = "Time10";
			public const string LineNumber = "LineNumber";
			public const string IsActive = "IsActive";
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
			lock (typeof(ConsumeMethodMetadata))
			{
				if (ConsumeMethodMetadata.mapDelegates == null)
				{
					ConsumeMethodMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ConsumeMethodMetadata.meta == null)
				{
					ConsumeMethodMetadata.meta = new ConsumeMethodMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeMethodName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TimeSequence", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SygnaText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IterationQty", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IterationInInterval", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Time01", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time02", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time03", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time04", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time05", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time06", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time07", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time08", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time09", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Time10", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LineNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "ConsumeMethod";
				meta.Destination = "ConsumeMethod";
				meta.spInsert = "proc_ConsumeMethodInsert";
				meta.spUpdate = "proc_ConsumeMethodUpdate";
				meta.spDelete = "proc_ConsumeMethodDelete";
				meta.spLoadAll = "proc_ConsumeMethodLoadAll";
				meta.spLoadByPrimaryKey = "proc_ConsumeMethodLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ConsumeMethodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
