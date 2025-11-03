<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PoliSoundDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.PoliSoundDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
            function openWinUpload() {
                var oWnd = window.$find("<%= winUpload.ClientID %>");
                oWnd.setUrl("PoliSoundUpload.aspx?suid=<%=txtServiceUnitID.Text%>");
                oWnd.show();
            }
            function onWinUploadClientClose(oWnd) {
                //Jika apply di click
                var result = oWnd.argument;
                if (result) {
                    window.$find("<%= txtSoundFilePath.ClientID %>").set_value(result);;
                }
                result = null;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="140px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Upload Sound File"
        OnClientClose="onWinUploadClientClose" ID="winUpload">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service unit id required."
                    ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvServiceUnitName" runat="server" ErrorMessage="Serviceunit name required."
                    ValidationGroup="entry" ControlToValidate="txtServiceUnitName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblQueueCode" runat="server" Text="Queueing Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtQueueCode" runat="server" Width="300px" MaxLength="4" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvQueueCode" runat="server" ErrorMessage="Queueing code required."
                    ValidationGroup="entry" ControlToValidate="txtQueueCode" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Registration Queueing"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRQueueingLocReg" runat="server" Width="300px"></telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Registration Queueing location required."
                    ValidationGroup="entry" ControlToValidate="cboSRQueueingLocReg" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Service Unit Queueing"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRQueueingLocPoli" runat="server" Width="300px"></telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Service Unit Queueing location required."
                    ValidationGroup="entry" ControlToValidate="cboSRQueueingLocPoli" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="File Suara"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSoundFilePath" runat="server" Width="100%" ReadOnly="true" />
            </td>
            <td width="20px">
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="javascript:openWinUpload();return false;" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
