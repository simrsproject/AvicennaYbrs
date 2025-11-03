<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitItemServiceCompDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitItemServiceCompDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemServiceCompMapping" runat="server" ValidationGroup="ServiceUnitItemServiceComp" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitItemServiceComp"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblTariffComponent" Text="Tariff Component" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboTariffComponentID" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvTariffComponentID" runat="server" ErrorMessage="Tariff Component required."
                            ValidationGroup="ServiceUnitItemServiceComp" ControlToValidate="cboTariffComponentID"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblRegType" Text="Registration Type" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboRegType" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRegType" runat="server" ErrorMessage="Registration Type required."
                            ValidationGroup="ServiceUnitItemServiceComp" ControlToValidate="cboRegType" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChartOfAccountIdIncome" runat="server" Text="COA Revenue"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncome" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboChartOfAccountIdIncome_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdIncome_ItemDataBound"
                            OnItemsRequested="cboChartOfAccountIdIncome_ItemsRequested">
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
                        <asp:Label ID="lblSubledgerIdIncome" runat="server" Text="Subledger Revenue"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncome" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdIncome_ItemDataBound"
                            OnItemsRequested="cboSubledgerIdIncome_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                    &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
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
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChartOfAccountIdDiscount" runat="server" Text="COA Discount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDiscount" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboChartOfAccountIdDiscount_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdDiscount_ItemDataBound"
                            OnItemsRequested="cboChartOfAccountIdDiscount_ItemsRequested">
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
                        <asp:Label ID="lblSubledgerIdDiscount" runat="server" Text="Subledger Discount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSubledgerIdDiscount" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdDiscount_ItemDataBound"
                            OnItemsRequested="cboSubledgerIdDiscount_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                    &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
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
                        <asp:Label ID="lblChartOfAccountIdCost" runat="server" Text="COA Cost Paramedic Fee"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCost" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdCost_SelectedIndexChanged"
                            OnItemDataBound="cboChartOfAccountIdCost_ItemDataBound" OnItemsRequested="cboChartOfAccountIdCost_ItemsRequested">
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
                        <asp:Label ID="lblSubledgerIdCost" runat="server" Text="Subledger Cost Paramedic Fee"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSubledgerIdCost" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdCost_ItemDataBound"
                            OnItemsRequested="cboSubledgerIdCost_ItemsRequested">
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
                        Account Group
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboAccountGroupID" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAccountGroup" runat="server" ErrorMessage="Account Group required."
                            ValidationGroup="ServiceUnitItemServiceComp" ControlToValidate="cboAccountGroupID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitItemServiceComp"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ServiceUnitItemServiceComp" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
