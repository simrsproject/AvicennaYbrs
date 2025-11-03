<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PatientDocumentEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PatientDocumentEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Document Date"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDocumentDate" runat="server" Width="100px" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
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
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine"
                    Height="200px" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="File Document"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFileAttachName" runat="server" Width="100%" ReadOnly="true" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Upload File"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAsyncUpload ID="uplFileTemplate" runat="server" ControlObjectsVisibility="None" 
                    Width="100%" InitialFileInputsCount="1" MaxFileInputsCount="1">
                </telerik:RadAsyncUpload>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>

</asp:Content>
