<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubLedgerDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.AcctSubGroup.SubLedgerDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSubLedger" runat="server" ValidationGroup="SubLedger" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SubLedger"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr>
        <td class="label">Subledger Code
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtSubLedgerName" runat="server" Width="300px" MaxLength="50" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSubLedgerName" runat="server" ErrorMessage="Subledger Code is required."
                ControlToValidate="txtSubLedgerName" SetFocusOnError="True" ValidationGroup="SubLedger" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>

    <tr>
        <td class="label">Description
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="50" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description is required."
                ControlToValidate="txtDescription" SetFocusOnError="True" ValidationGroup="SubLedger" Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Head Office Sub Ledger
        </td>
        <td class="entry">
            <%--fungsi cboHoSubledgerId_ClientItemsRequesting ada di parent page--%>
            <telerik:RadComboBox ID="cboHoSubLedgerId" runat="server" Width="300px" EmptyMessage="Select a HO Sub Ledger"
                                 EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" >
                <WebServiceSettings Method="SubLedger" />
                <ClientItemTemplate>
                    <div>
                        <ul class="details">
                            <li class="bold"><span>#= Text # </span></li>
                            <li class="smaller"><span>#= Attributes.GroupName #  </span></li>
                        </ul>
                    </div>
                </ClientItemTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr runat="server" id="trDirectCost">
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsDirectCost" runat="server" Text="Direct Cost" />
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SubLedger"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="SubLedger"
                Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel"></asp:Button></td>
    </tr>
</table>

