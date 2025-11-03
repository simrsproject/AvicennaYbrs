/* Use JqueryUI Theme*/
var _ctrlHideEffect = "fade";
var _ctrlHideEffectDuration = 300;
var _ctrlShowEffect = "fade";//"fade";
var _ctrlShowEffectDuration = 1000;

/* Custom Html Message Constant */
var _icoLoadingDefault = "fas fa-compass";
var _icoLoading = ["fas fa-asterisk", "fas fa-spinner", "far fa-star", "fas fa-circle-notch", "far fa-compass", "fas fa-futbol", "fas fa-life-ring", "fas fa-radiation","fas fa-yin-yang"];
var _htmlConfirm = "<div class='confirmationDialog'>Are you sure want to delete?</div>";
var _htmlConfirmDoFn = "<div class='confirmationDialog'>Please confirm!</div>";
var _htmlDeleting = "<h3><span class='" + _icoLoadingDefault + " fa-lg fa-spin'></span> Deleting...</h3>";
var _htmlLoading = "<h3><span class='" + _icoLoadingDefault + " fa-lg fa-spin'></span> Loading...</h3>";
var _htmlLoadingSmall = "<span class='" + _icoLoadingDefault + " fa-lg fa-spin'></span> Loading...";
var _htmlProgress = "Please wait...";

var _htmlDlgHeaderProgress = "<h3 class='text-warning'><span class='" + _icoLoadingDefault + " fa-lg fa-spin'></span> Executing</h3>";
var _htmlDlgHeaderErr = "<h3 class='text-danger'><span class='fa fa-exclamation-triangle fa-lg'></span> Error</h3>";
var _htmlDlgHeaderInfo = "<h3 class='text-info'><span class='fa fa-info-circle fa-lg'></span> Info</h3>";
var _htmlDlgHeaderSuccess = "<h3 class='text-success'><span class='fas fa-check fa-lg'></span> Success</h3>";
var _htmlDlgHeaderConfirm = "<h3 class='text-warning'><span class='fas fa-question-circle fa-lg'></span> Confirmation</h3>";

// http json responds code, please do not edit
var _sJsonRequestErrStatus = "ERR";
var _sJsonRequestOkStatus = "OK";

var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];