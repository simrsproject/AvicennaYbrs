<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovementHistoryEditor.ascx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.MovementHistoryEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMovementHistoryItem" runat="server" ValidationGroup="MovementHistoryItem" />

<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MovementHistoryItem" ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:Label ID="lblAssetId" runat="server" Visible="false"></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 5%; vertical-align: top;">&nbsp;</td>
        <td style="width: 45%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label" style="width: 79px">
                        Transaction No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="89%" ReadOnly="true" />
                        <asp:Label runat="server" ID="lblTransactionNo" />
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Transaction No required."
                            ControlToValidate="txtTransactionNo" SetFocusOnError="True" ValidationGroup="MovementHistoryItem"
                            Width="100%">
                            <asp:Image runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        From Service Unit
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadComboBox runat="server" ID="cboFromServiceUnit" Height="190px" Width="90%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="height: 15px; width: 20px;">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="From Service unit required"
                            ValidationGroup="MovementHistoryItem" ControlToValidate="cboFromServiceUnit" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        From Location
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadComboBox runat="server" ID="cboFromLocation" Height="190px" Width="90%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "LocationID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "LocationName")%>
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="height: 15px; width: 20px;">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="From Location required"
                            ValidationGroup="MovementHistoryItem" ControlToValidate="cboFromLocation" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        Description
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadTextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="89%"
                            MaxLength="250" Height="60px" />
                    </td>
                    <td style="height: 15px; width: 20px;">
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 45%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label" style="width: 16px">
                        Movement Date
                    </td>
                    <td class="entry" style="width: 361px">
                        <telerik:RadDatePicker ID="txtMovementDate" runat="server" Width="105px">
                            <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                            <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Movement Date required"
                            ValidationGroup="MovementHistoryItem" ControlToValidate="txtMovementDate" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        To Service Unit
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadComboBox runat="server" ID="cboToServiceUnit" Height="190px" Width="90%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="height: 15px; width: 20px;">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="To Service unit required"
                            ValidationGroup="MovementHistoryItem" ControlToValidate="cboToServiceUnit" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        To Location
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadComboBox runat="server" ID="cboToLocation" Height="190px" Width="90%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "LocationID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "LocationName")%>
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="height: 15px; width: 20px;">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="To Location required"
                            ValidationGroup="MovementHistoryItem" ControlToValidate="cboToLocation" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="entry" align="center" style="width: 361px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="MovementHistoryItem" Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="MovementHistoryItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" />
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
            </table>
            
        </td>
    </tr>
</table>
<br /><br />