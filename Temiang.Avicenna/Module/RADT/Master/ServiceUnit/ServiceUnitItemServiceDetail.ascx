<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitItemServiceDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitItemServiceDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitItemService" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitItemService"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItem" runat="server" Text="Item Service"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested">
                            <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ServiceUnitItemService"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="trIdiCode">
                    <td class="label">
                        <asp:Label ID="lblIdiCode" runat="server" Text="Item IDI Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboIdiCode" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemDataBound="cboIdiCode_ItemDataBound" OnItemsRequested="cboIdiCode_ItemsRequested">
                            <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "IdiName") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
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
                        <asp:CheckBox ID="chkIsAllowEditByUserVerificated" runat="server" Text="Allow Edit By User Verificated" />
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
                        <asp:CheckBox ID="chkIsVisible" runat="server" Text="Visible To User Entry" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitItemService"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ServiceUnitItemService" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top; width: 50%">
            <table width="100%">
                <tr>
                    <td >
                        <asp:Label ID="lblChartOfAccountId" runat="server" Text="Chart Of Account" visible="false"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Height="190px" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                            OnItemsRequested="cboChartOfAccountId_ItemsRequested" visible="false">
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
                    <td >
                        <asp:Label ID="lblSubledgerId" runat="server" Text="Subledger" visible="false"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSubledgerId" Height="190px" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                            OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerId_ItemsRequested" visible="false">
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
    </tr>
</table>
