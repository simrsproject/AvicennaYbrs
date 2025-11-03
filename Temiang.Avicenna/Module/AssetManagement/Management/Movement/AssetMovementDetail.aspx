<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="AssetMovementDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.AssetMovementDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Service Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnit" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboServiceUnit_ItemDataBound"
                                OnItemsRequested="cboFromServiceUnit_ItemsRequested" OnSelectedIndexChanged="cboFromServiceUnit_SelectedIndexChanged">
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnit" runat="server" ErrorMessage="From Service Unit is required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Room
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboFromLocation" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboFromLocation_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Asset
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAssetID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetID_ItemDataBound"
                                OnItemsRequested="cboAssetID_ItemsRequested" OnSelectedIndexChanged="cboAssetID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "AssetName")%>
                                    </b>
                                    <br />
                                    Serial No :
                                    <%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
                                    <br />
                                    Location :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    <br />
                                    Unit Maintenance :&nbsp;<%# DataBinder.Eval(Container.DataItem, "MaintenanceServiceUnitName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssetId" runat="server" ErrorMessage="Asset is required."
                                ControlToValidate="cboAssetID" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Asset Group
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssetGroup" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Model Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBrandName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Serial Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSerialNumber" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Purchase Date
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPurchaseDate2" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="ASSET MOVEMENT CHANGE" Font-Bold="True"
                            Font-Size="9"></asp:Label></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            Transaction No
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                                            <asp:Label runat="server" ID="lblTransactionNo" />
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Transaction No required."
                                                ControlToValidate="txtTransactionNo" SetFocusOnError="True" ValidationGroup="entry"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Movement Date
                                        </td>
                                        <td class="entry">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Movement Date is required."
                                                ValidationGroup="entry" ControlToValidate="txtMovementDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            To Service Unit
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadComboBox ID="cboToServiceUnit" runat="server" Width="300px" HighlightTemplatedItems="True"
                                                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboServiceUnit_ItemDataBound"
                                                OnItemsRequested="cboToServiceUnit_ItemsRequested" OnSelectedIndexChanged="cboToServiceUnit_SelectedIndexChanged">
                                                <FooterTemplate>
                                                    Note : Show max 20 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="To Service Unit is required."
                                                ValidationGroup="entry" ControlToValidate="cboToServiceUnit" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            To Room
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadComboBox runat="server" ID="cboToLocation" Width="300px" AllowCustomText="true"
                                                Filter="Contains">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Notes
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="300px"
                                                MaxLength="250" Height="60px" />
                                        </td>
                                        <td style="width: 20px;">
                                            <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Notes is required."
                                                ValidationGroup="entry" ControlToValidate="txtDescription" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="label">
                                            <asp:Label ID="lblApprove" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" Enabled="false" />&nbsp;
                                            <asp:CheckBox ID="chkIsDeleted" Text="Deleted" runat="server" Enabled="false" Visible="false" />
                                        </td>
                                        <td style="width: 20px;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
