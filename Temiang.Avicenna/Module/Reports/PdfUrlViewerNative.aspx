<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PdfUrlViewerNative.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.PdfUrlViewerNative" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript" src="<%=Helper.UrlRoot() %>/JavaScript/jquery.js"></script>
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function Close() {
                var oWindow = GetRadWindow();
                if (oWindow) {
                    oWindow.close();
                }
                else {
                    window.close();
                }
            }

            function saveToGuarantorDoc() {
                $.ajax({
                    type: 'POST',
                    url: "PdfUrlViewer.aspx/SaveToGuarantorDoc",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    data: "{'mode':'<%= Request.QueryString["mode"] %>','id':'<%= Request.QueryString["trno"] %>'}",
                    success: function (response) {
                        obj = JSON.parse(response);
                        alert(obj.d);
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = xhr.status + ': ' + xhr.statusText
                        alert('Error - ' + errorMessage);
                    }
                });
            }
        </script>
        <div style="width:100%;">
            <button  style="float: right;" type="button" onclick="saveToGuarantorDoc()">Save to Guarantor Document</button>
            <br />
            <br />
        </div>
        <iframe id="pdfViewer" width="99%"  src="<%=string.Format("PdfUrlViewerHandler.ashx?mode={0}&id={1}&trno={2}&seqno={3}",Request.QueryString["mode"], Request.QueryString["id"], Request.QueryString["trno"], Request.QueryString["seqno"]) %>"></iframe>

        <script type="text/javascript">
            function applyResize() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) - 36;

                document.getElementById('pdfViewer').setAttribute("style", "height:" + height + "px");
            }
            window.onload = function () {
                applyResize();
            }
            window.onresize = function () {
                applyResize();
            }
        </script>
    </form>
</body>
</html>
