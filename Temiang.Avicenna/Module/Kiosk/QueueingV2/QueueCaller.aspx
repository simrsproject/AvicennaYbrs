<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="QueueCaller.aspx.cs" Inherits="Temiang.Avicenna.Module.Kiosk.QueueingV2.QueueCaller" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">

    </style>
    <script type="text/javascript">
        //$ = $telerik.$; 

        var BaseURL = '<%=BaseURL()%>';
        $(document).ready(function () {
            LoadQueueingCaller();
        });

        function LoadQueueingCaller() {
            $.ajax({
                type: 'POST',
                url: BaseURL + "/Queueing/HomePage",
                data: {},
                success: function (data) {
                    $('#divMain').html(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //ShowError(xhr.responseText);
                    alert(xhr.responseText);
                },
                dataType: 'html'
            });
        }
    </script>
    <div id="divMain"></div>
</asp:Content>
