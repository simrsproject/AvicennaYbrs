<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicFeeAddDeducItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeAddDeducItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ErrorMessage="" ID="customValidator" OnServerValidate="customValidator_ServerValidate"
    runat="server" ValidationGroup="MemorialItem">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        Account Code
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="txtChartOfAccountCode" TabIndex="0" runat="server" Height="300px"
                            Width="300px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="ChartOfAccountCode"
                            DataValueField="ChartOfAccountId" NoWrap="true" EnableLoadOnDemand="true">
                            <ItemTemplate>
                                <div>
                                    <b>
                                        <%# Eval("ChartOfAccountCode")%></b>&nbsp;-&nbsp;<%# Eval("ChartOfAccountName") %>
                                </div>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Account Code required."
                            ControlToValidate="txtChartOfAccountCode" SetFocusOnError="True" ValidationGroup="MemorialItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Subledger
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="ddlSubLedger" TabIndex="0" runat="server" Height="190px"
                            Width="300px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="Description"
                            DataValueField="SubLedgerId" NoWrap="true" EnableLoadOnDemand="true">
                            <ItemTemplate>
                                <div>
                                    <b><%# Eval("SubLedgerName")%></b>&nbsp;-&nbsp;<%# Eval("Description") %>
                                </div>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        Amount
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" Value="0" Width="100px">
                            <EnabledStyle HorizontalAlign="Right" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="MemorialItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MemorialItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
