<%@ Control Language="C#" AutoEventWireup="true" Codebehind="EDCMachineTariffDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.EDCMachineTariffDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEDCMachineTariff" runat="server" ValidationGroup="EDCMachineTariff" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EDCMachineTariff"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width:50%">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCardType" runat="server" Text="Card Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCardType" runat="server" Width="304px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRCardType" runat="server" ErrorMessage="Card Type required."
                            ControlToValidate="cboSRCardType" SetFocusOnError="True" ValidationGroup="EDCMachineTariff"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEDCMachineTariff" runat="server" Text="Card Fee (%)"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEDCMachineTariff" runat="server" Width="100px" Type="Percent" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEDCMachineTariff" runat="server" ErrorMessage="EDC Machine Fee required."
                            ControlToValidate="txtEDCMachineTariff" SetFocusOnError="True" ValidationGroup="EDCMachineTariff"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAddFeeAmount" runat="server" Text="Add Card Fee Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAddFeeAmount" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAddFeeAmount" runat="server" ErrorMessage="Add Card Fee Amount required."
                            ControlToValidate="txtAddFeeAmount" SetFocusOnError="True" ValidationGroup="EDCMachineTariff"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsChargedToPatient" runat="server" Text="Charged To Patient" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:50%">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChartOfAccountId" runat="server" Text="Card Fee Chart Of Account"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Height="190px" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged"
                            OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubledgerId" runat="server" Text="Card Fee Subledger"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSubledgerId" Height="190px" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                            OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerId_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%> 
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                    </td>
                </tr> 
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EDCMachineTariff"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="EDCMachineTariff" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
