using System.Configuration;
using System.Web;

namespace Temiang.Avicenna.BusinessObject.Util {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DataConsolidationSoap", Namespace="http://tempuri.org/")]
    public partial class DataConsolidationSoap : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        
        private System.Threading.SendOrPostCallback CommitDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadDataMasterOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadDataTransactionOperationCompleted;
        
        private System.Threading.SendOrPostCallback CommitDataClosingJournalOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public DataConsolidationSoap()
        {

            this.Url = ConsolidationWebServiceUrlLocation;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        private static string ConsolidationWebServiceUrlLocation
        {
            get
            {
                if (HttpContext.Current.Session["c_consurl"] == null)
                {
                    var value = ConfigurationManager.AppSettings["ConsolidationWebServiceUrlRoot"];
                    HttpContext.Current.Session["c_consurl"] = string.Format("{0}//WebService/DataConsolidation.asmx", value);
                }
                return (string)HttpContext.Current.Session["c_consurl"];
            }
        }
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event CommitDataCompletedEventHandler CommitDataCompleted;
        
        /// <remarks/>
        public event DownloadDataMasterCompletedEventHandler DownloadDataMasterCompleted;
        
        /// <remarks/>
        public event DownloadDataTransactionCompletedEventHandler DownloadDataTransactionCompleted;
        
        /// <remarks/>
        public event CommitDataClosingJournalCompletedEventHandler CommitDataClosingJournalCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CommitData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CommitData(string token, string healthcareID, string dataTransfer, bool isManualLog) {
            object[] results = this.Invoke("CommitData", new object[] {
                        token,
                        healthcareID,
                        dataTransfer,
                        isManualLog});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CommitDataAsync(string token, string healthcareID, string dataTransfer, bool isManualLog) {
            this.CommitDataAsync(token, healthcareID, dataTransfer, isManualLog, null);
        }
        
        /// <remarks/>
        public void CommitDataAsync(string token, string healthcareID, string dataTransfer, bool isManualLog, object userState) {
            if ((this.CommitDataOperationCompleted == null)) {
                this.CommitDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCommitDataOperationCompleted);
            }
            this.InvokeAsync("CommitData", new object[] {
                        token,
                        healthcareID,
                        dataTransfer,
                        isManualLog}, this.CommitDataOperationCompleted, userState);
        }
        
        private void OnCommitDataOperationCompleted(object arg) {
            if ((this.CommitDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CommitDataCompleted(this, new CommitDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadDataMaster", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DownloadDataMaster(string token, string requestByHealthcareID) {
            object[] results = this.Invoke("DownloadDataMaster", new object[] {
                        token,
                        requestByHealthcareID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadDataMasterAsync(string token, string requestByHealthcareID) {
            this.DownloadDataMasterAsync(token, requestByHealthcareID, null);
        }
        
        /// <remarks/>
        public void DownloadDataMasterAsync(string token, string requestByHealthcareID, object userState) {
            if ((this.DownloadDataMasterOperationCompleted == null)) {
                this.DownloadDataMasterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadDataMasterOperationCompleted);
            }
            this.InvokeAsync("DownloadDataMaster", new object[] {
                        token,
                        requestByHealthcareID}, this.DownloadDataMasterOperationCompleted, userState);
        }
        
        private void OnDownloadDataMasterOperationCompleted(object arg) {
            if ((this.DownloadDataMasterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadDataMasterCompleted(this, new DownloadDataMasterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadDataTransaction", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DownloadDataTransaction(string token, string requestByHealthcareID) {
            object[] results = this.Invoke("DownloadDataTransaction", new object[] {
                        token,
                        requestByHealthcareID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadDataTransactionAsync(string token, string requestByHealthcareID) {
            this.DownloadDataTransactionAsync(token, requestByHealthcareID, null);
        }
        
        /// <remarks/>
        public void DownloadDataTransactionAsync(string token, string requestByHealthcareID, object userState) {
            if ((this.DownloadDataTransactionOperationCompleted == null)) {
                this.DownloadDataTransactionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadDataTransactionOperationCompleted);
            }
            this.InvokeAsync("DownloadDataTransaction", new object[] {
                        token,
                        requestByHealthcareID}, this.DownloadDataTransactionOperationCompleted, userState);
        }
        
        private void OnDownloadDataTransactionOperationCompleted(object arg) {
            if ((this.DownloadDataTransactionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadDataTransactionCompleted(this, new DownloadDataTransactionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CommitDataClosingJournal", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int CommitDataClosingJournal(string accountBalance, string editedBy) {
            object[] results = this.Invoke("CommitDataClosingJournal", new object[] {
                        accountBalance,
                        editedBy});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void CommitDataClosingJournalAsync(string accountBalance, string editedBy) {
            this.CommitDataClosingJournalAsync(accountBalance, editedBy, null);
        }
        
        /// <remarks/>
        public void CommitDataClosingJournalAsync(string accountBalance, string editedBy, object userState) {
            if ((this.CommitDataClosingJournalOperationCompleted == null)) {
                this.CommitDataClosingJournalOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCommitDataClosingJournalOperationCompleted);
            }
            this.InvokeAsync("CommitDataClosingJournal", new object[] {
                        accountBalance,
                        editedBy}, this.CommitDataClosingJournalOperationCompleted, userState);
        }
        
        private void OnCommitDataClosingJournalOperationCompleted(object arg) {
            if ((this.CommitDataClosingJournalCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CommitDataClosingJournalCompleted(this, new CommitDataClosingJournalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CommitDataCompletedEventHandler(object sender, CommitDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CommitDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CommitDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void DownloadDataMasterCompletedEventHandler(object sender, DownloadDataMasterCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadDataMasterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadDataMasterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void DownloadDataTransactionCompletedEventHandler(object sender, DownloadDataTransactionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadDataTransactionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadDataTransactionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CommitDataClosingJournalCompletedEventHandler(object sender, CommitDataClosingJournalCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CommitDataClosingJournalCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CommitDataClosingJournalCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591