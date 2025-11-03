<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AbRestrictionSuggestionDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.AbRestrictionSuggestionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDiagnose" runat="server" ValidationGroup="Suggestion" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Suggestion"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Suggestion
        </td>
        <td >
            <telerik:RadEditor runat="server" ID="edtSuggestionNote" Width="700px" Height="500px">
            </telerik:RadEditor>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                ControlToValidate="edtSuggestionNote" SetFocusOnError="True" ValidationGroup="Suggestion"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Stratification
        </td>
        <td >
            <telerik:RadComboBox ID="cboAbLevel" runat="server" Width="150px" EmptyMessage="Select Stratification">
                <Items>
                    <telerik:RadComboBoxItem Text="I" Value="1" />
                    <telerik:RadComboBoxItem Text="II" Value="2" />
                    <telerik:RadComboBoxItem Text="III" Value="3" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Stratification required."
                ControlToValidate="cboAbLevel" SetFocusOnError="True" ValidationGroup="Suggestion"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>

    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="Suggestion"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="Suggestion" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
