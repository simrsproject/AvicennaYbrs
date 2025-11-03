<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="AssetAuctionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.AssetAuctionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnit" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="False" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboServiceUnit_ItemDataBound"
                                OnItemsRequested="cboFromServiceUnit_ItemsRequested">
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
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoom" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboFromLocation" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAsset" runat="server" Text="Asset"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAssetID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="False" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetID_ItemDataBound"
                                OnItemsRequested="cboAssetID_ItemsRequested">
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
                            <asp:RequiredFieldValidator ID="rfvAssetID" runat="server" ErrorMessage="Asset is required."
                                ValidationGroup="entry" ControlToValidate="cboAssetID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry" style="height: 15px;">
                            <asp:CheckBox ID="chkIsFixedAssetFrom" runat="server" Text="Fixed Asset" Enabled="False" />
                        </td>
                        <td style="height: 15px; width: 20px;"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssetGroup" runat="server" Text="Asset Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssetGroup" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblModelNumber" runat="server" Text="Model Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBrandName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSerialNumber" runat="server" Text="Serial Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSerialNumber" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPurchaseDate" runat="server" Text="Purchase Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPurchaseDate2" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
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
                        <asp:Label ID="Label3" runat="server" Text="ASSET AUCTION" Font-Bold="True"
                            Font-Size="9"></asp:Label></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTransactionDate" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtDate" runat="server" Width="105px" Enabled="False">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Date is required."
                                                ValidationGroup="entry" ControlToValidate="txtDate" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr style="display:none">
                                        <td class="label">
                                            <asp:Label ID="lblCurrentValue" runat="server" Text="Current Value"></asp:Label>
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadNumericTextBox ID="txtCurrentValue" runat="server" Value="0" Width="100px" Enabled="false">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr style="display:none">
                                        <td class="label">Depreciation Accumulation Value
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadNumericTextBox ID="txtDepreciationAccValue" runat="server" Value="0" Width="100px"
                                                Enabled="False">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSalesPrice" runat="server" Text="Price"></asp:Label>
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadNumericTextBox ID="txtSalesPrice" runat="server" Value="0" Width="100px" AutoPostBack="true" OnTextChanged="txtSalesPrice_TextChanged">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="height: 15px; width: 20px;">
                                            <asp:RequiredFieldValidator ID="rfvSalesPrice" runat="server" ErrorMessage="Price is required."
                                                ValidationGroup="entry" ControlToValidate="txtSalesPrice" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label"></td>
                                        <td>
                                            <asp:RadioButtonList ID="rblTaxStatus" runat="server" RepeatDirection="Horizontal"
                                                OnTextChanged="rblTaxStatus_OnTextChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="true">Exclude Tax</asp:ListItem>
                                                <asp:ListItem>Include Tax</asp:ListItem>
                                                <asp:ListItem>No Tax</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTaxPercentage" runat="server" Text="Tax Percentage" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTaxPercentage" runat="server" Type="Percent" Width="100px"
                                                MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="true" />
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="100px" MaxLength="16"
                                                MinValue="0" ReadOnly="true" />
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTotal" runat="server" Text="Total" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRPaymentType" runat="server" Text="Payment Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRPaymentType" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboSRPaymentType_SelectedIndexChanged" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvSRPaymentType" runat="server" ErrorMessage="Payment Type required."
                                                ControlToValidate="cboSRPaymentType" SetFocusOnError="True" ValidationGroup="entry"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRPaymentMethod" runat="server" Text="Payment Method"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRPaymentMethod" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboSRPaymentMethod_SelectedIndexChanged" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboBankID" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBuyersName" runat="server" Text="Buyer's Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBuyersName" runat="server" Width="300px" MaxLength="200" />
                                        </td>
                                        <td style="width: 20px;">
                                            <asp:RequiredFieldValidator ID="rfvBuyersName" runat="server" ErrorMessage="Buyer's Name is required."
                                                ValidationGroup="entry" ControlToValidate="txtBuyersName" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBuyersAddress" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBuyersAddress" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                                        </td>
                                        <td style="width: 20px;">
                                            <asp:RequiredFieldValidator ID="rfvBuyersAddress" runat="server" ErrorMessage="Address is required."
                                                ValidationGroup="entry" ControlToValidate="txtBuyersAddress" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBuyersPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBuyersPhoneNo" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td style="width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBuyersTaxRegister" runat="server" Text="Tax Register"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBuyersTaxRegister" runat="server" Width="300px" MaxLength="100" />
                                        </td>
                                        <td style="width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px"
                                                MaxLength="500" Height="60px" />
                                        </td>
                                        <td style="width: 20px;"></td>
                                        <td></td>
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
