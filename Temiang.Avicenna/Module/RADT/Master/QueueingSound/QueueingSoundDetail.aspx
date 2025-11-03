<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="QueueingSoundDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QueueingSoundDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="140px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Upload Document Template"
        OnClientClose="onWinUploadClientClose" ID="winUpload">
    </telerik:RadWindow>
    <table width="100%">
        <tr style="display:none">
            <td class="label">
                <asp:Label ID="lblSoundID" runat="server" Text="Sound ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtSoundID" NumberFormat-DecimalDigits="0" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSoundID" runat="server" ErrorMessage="Sound ID required."
                    ValidationGroup="entry" ControlToValidate="txtSoundID" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblName" runat="server" Text="File Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtName" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="File Name required."
                    ValidationGroup="entry" ControlToValidate="txtName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNumber" runat="server" Text="Number"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtNumber" runat="server" NumberFormat-DecimalDigits="0" Width="300px" />
            </td>
            <td></td>
        </tr>
         <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Available As Service Counter"></asp:Label>
            </td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsServiceCounter" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFIlePath" runat="server" Text="File Path"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFilePath" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td width="20px">
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="javascript:openWinUpload();return false;" />
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            function openWinUpload() {
                var oWnd = window.$find("<%= winUpload.ClientID %>");
                oWnd.setUrl("QueueingSoundUpload.aspx?soid=<%=txtSoundID.Text%>");
                oWnd.show();
            }
            function onWinUploadClientClose(oWnd) {
                //Jika apply di click
                var result = oWnd.argument;
                if (result) {
                    window.$find("<%= txtFilePath.ClientID %>").set_value(result);;
                }
                result = null;
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>