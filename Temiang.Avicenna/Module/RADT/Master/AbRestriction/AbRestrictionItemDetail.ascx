<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AbRestrictionItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.AbRestrictionItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDiagnose" runat="server" ValidationGroup="Item" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Item"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Zat Active
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboZatActiveID" runat="server" Width="100%" EmptyMessage="Select a Item"
                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                <WebServiceSettings Method="ZatActives" Path="~/WebService/ComboBoxDataService.asmx" />
                <ClientItemTemplate>
                     <div>
                        <ul class="details">
                            <li class="bold"><span>#= Text # </span></li>
                        </ul>
                    </div>
                </ClientItemTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Zat Active required."
                ControlToValidate="cboZatActiveID" SetFocusOnError="True" ValidationGroup="Item"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Stratification
        </td>
        <td class="entry">
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
                ControlToValidate="cboAbLevel" SetFocusOnError="True" ValidationGroup="Item"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Notes
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" MaxLength="300" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="Item"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="Item" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
