<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ReferEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ReferEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table width="100%">
        <tr>
            <td class="label">Refer Date
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtReferDate" runat="server" Width="100px" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Service Unit
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboReferServiceUnitID" runat="server" Width="304px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboReferServiceUnitID_SelectedIndexChanged" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvReferServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                    ValidationGroup="entry" ControlToValidate="cboReferServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Physician
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboReferParamedicID" runat="server" Width="304px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboReferParamedicID_SelectedIndexChanged" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvReferParamedicID" runat="server" ErrorMessage="Physician required."
                    ValidationGroup="entry" ControlToValidate="cboReferParamedicID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Room
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboReferRoomID" runat="server" Width="304px" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Que No
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboReferQue" runat="server" Width="304px" />
            </td>
            <td width="20px" />
        </tr>
        <tr>
            <td class="label">Examination Result
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Width="300px" MaxLength="500"
                    TextMode="MultiLine" />
            </td>
            <td width="20px" />
        </tr>
        <tr>
            <td class="label">Consultation Notes
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReferNotes" runat="server" Width="300px" MaxLength="500"
                    TextMode="MultiLine" />
            </td>
            <td width="20px" />
        </tr>
    </table>
</asp:Content>
