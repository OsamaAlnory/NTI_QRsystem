﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebService
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MSSQL400028")]
	public partial class MSSQL400028DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public MSSQL400028DataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["MSSQL400028ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MSSQL400028DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MSSQL400028DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MSSQL400028DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MSSQL400028DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Account> Accounts
		{
			get
			{
				return this.GetTable<Account>();
			}
		}
		
		public System.Data.Linq.Table<Info> Infos
		{
			get
			{
				return this.GetTable<Info>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Accounts")]
	public partial class Account
	{
		
		private int _ID;
		
		private string _Username;
		
		private string _Password;
		
		private string _Class;
		
		private bool _isAdmin;
		
		public Account()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this._Username = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this._Password = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Class", DbType="VarChar(20)")]
		public string Class
		{
			get
			{
				return this._Class;
			}
			set
			{
				if ((this._Class != value))
				{
					this._Class = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_isAdmin", DbType="Bit NOT NULL")]
		public bool isAdmin
		{
			get
			{
				return this._isAdmin;
			}
			set
			{
				if ((this._isAdmin != value))
				{
					this._isAdmin = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Info")]
	public partial class Info
	{
		
		private int _ID;
		
		private string _Studentname;
		
		private System.Nullable<System.TimeSpan> _ATime;
		
		private System.Nullable<System.DateTime> _Date;
		
		public Info()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Studentname", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string Studentname
		{
			get
			{
				return this._Studentname;
			}
			set
			{
				if ((this._Studentname != value))
				{
					this._Studentname = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ATime", DbType="Time")]
		public System.Nullable<System.TimeSpan> ATime
		{
			get
			{
				return this._ATime;
			}
			set
			{
				if ((this._ATime != value))
				{
					this._ATime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date")]
		public System.Nullable<System.DateTime> Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this._Date = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
