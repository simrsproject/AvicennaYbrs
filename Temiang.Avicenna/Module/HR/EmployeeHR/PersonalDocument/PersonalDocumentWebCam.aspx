<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true" CodeBehind="PersonalDocumentWebCam.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalDocumentWebCam" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnEmployeeNo" />

    <script type="text/javascript" language="javascript">
        var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
        var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;
        var _height = 0;
        var _width = 0;
        function onWinWebCamClose(sender, eventArgs) {

            // Restore window size
            var curWnd = GetRadWindow();
            if (_width > 0) {
                curWnd.setSize(_width, _height);
                curWnd.center();
            }

            // Get retval
            var arg = eventArgs.get_argument();
            if (arg) {
                var img = document.getElementById("imgFromWebCam");
                img.setAttribute('src', arg);
                var hdnImgData = document.getElementById("<%=hdnImgData.ClientID%>");
                hdnImgData.value = arg;
            }
        }


        function openWinWebCam() {

            // Resize window
            var curWnd = GetRadWindow();
            var curWndBounds = curWnd.getWindowBounds();
            // Not Maximized
            if (curWndBounds.y > 0 & curWndBounds.x > 0) {
                _height = curWndBounds.height;
                _width = curWndBounds.width;

                var setHeight = _wcHeight + 80;
                if (setHeight < _height)
                    setHeight = _height;

                var setWidth = _wcWidth + 40;
                if (setWidth < _width)
                    setWidth = _width;

                curWnd.setSize(setWidth, setHeight);
                curWnd.center();
            }

            var oWnd = $find("<%= winWebCam.ClientID %>");
            oWnd.setUrl("WebCamJCrop.aspx");
            //oWnd.setUrl("WebCam.aspx");

            oWnd.setSize(_wcWidth+40, _wcHeight+80);
            oWnd.center();
            oWnd.show();
        }

        <%= !IsPostBack && DataModeCurrent==AppEnum.DataMode.New? "$(window).bind(\"load\", function() {openWinWebCam();});": string.Empty %>
    </script>

    <telerik:RadWindow ID="winWebCam" Width="680px" Height="620px" runat="server" Modal="true"
        ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
        OnClientClose="onWinWebCamClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnPdId" />
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Document Date"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDocumentDate" runat="server" Width="100px" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr runat="server" id="rowUploadFile">
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Image"></asp:Label>
            </td>
            <td class="entry">
                <asp:HiddenField runat="server" ID="hdnImgData" />
                <img id="imgFromWebCam" style="width: auto; height: 100%;" />
                <br />
                <asp:Button runat="server" Text="Open WebCam" ID="btnWebCam" Width="315px"
                    OnClientClick="openWinWebCam();return false;" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDocumentName" runat="server" Text="Document Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDocumentName" runat="server" Width="100%" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDocumentName" runat="server" ErrorMessage="Document Name required."
                    ValidationGroup="entry" ControlToValidate="txtDocumentName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine"
                    Height="200px" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>

    </table>

</asp:Content>
