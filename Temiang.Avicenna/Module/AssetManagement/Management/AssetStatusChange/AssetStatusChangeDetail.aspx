<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="AssetStatusChangeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.AssetStatusChangeDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFromServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromLocation" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFromLocation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboAssetID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSerialNumber" />
                    <telerik:AjaxUpdatedControl ControlID="txtAssetGroup" />
                    <telerik:AjaxUpdatedControl ControlID="txtPurchaseDate2" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRAssetsStatusFrom" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsFixedAssetFrom" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsFixedAssetTo" />
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtDepreciationAccValue" />
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRAssetsStatusTo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtDepreciationAccValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsFixedAssetTo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSeqNo" runat="server" ErrorMessage="Seq No required."
                                ValidationGroup="entry" ControlToValidate="txtSeqNo" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
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
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboFromLocation_SelectedIndexChanged">
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
                                    Asset ID :&nbsp;<%# DataBinder.Eval(Container.DataItem, "AssetID")%>
                                    <br />
                                    Serial No :&nbsp;<%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
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
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry" style="height: 15px;">
                            <asp:CheckBox ID="chkIsFixedAssetFrom" runat="server" Text="Fixed Asset" Enabled="False" />
                        </td>
                        <td style="height: 15px; width: 20px;">
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
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
                        <asp:Label ID="Label3" runat="server" Text="ASSET STATUS CHANGE" Font-Bold="True"
                            Font-Size="9"></asp:Label></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            Date
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtDate" runat="server" Width="105px" Enabled="False">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Date is required."
                                                ValidationGroup="entry" ControlToValidate="txtDate" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            From Status
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadComboBox runat="server" ID="cboSRAssetsStatusFrom" Width="300px" AllowCustomText="true"
                                                Filter="Contains" Enabled="False">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            To Status
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadComboBox runat="server" ID="cboSRAssetsStatusTo" Width="300px" AllowCustomText="true"
                                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRAssetsStatusTo_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Current Value
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadNumericTextBox ID="txtCurrentValue" runat="server" Value="0" Width="150px"
                                                Enabled="False">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td class="label">
                                            Depreciation Accumulation Value
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadNumericTextBox ID="txtDepreciationAccValue" runat="server" Value="0" Width="150px"
                                                Enabled="False">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <asp:CheckBox ID="chkIsFixedAssetTo" runat="server" Text="Fixed Asset" Enabled="False" />
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Notes
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px"
                                                MaxLength="500" Height="60px" />
                                        </td>
                                        <td style="width: 20px;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
