<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="WorkOrderDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderNo" runat="server" Text="Work Order No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ErrorMessage="Work Order No required."
                                ValidationGroup="entry" ControlToValidate="txtOrderNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ErrorMessage="Date required."
                                ValidationGroup="entry" ControlToValidate="txtOrderDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Request Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Request Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="To Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboToServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="To Unit required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRequiredDate" runat="server" Text="Required Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtRequiredDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRequiredDate" runat="server" ErrorMessage="Required Date required."
                                ValidationGroup="entry" ControlToValidate="txtRequiredDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPMNo" runat="server" Text="PM No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPMNo" runat="server" Width="282px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsPreventiveMaintenance" Text="" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWorkType" runat="server" Text="Work Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRWorkType" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSRWorkType_ItemDataBound"
                                OnItemsRequested="cboSRWorkType_ItemsRequested" AutoPostBack="true">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    </b>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRWorkType" runat="server" ErrorMessage="Work Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRWorkType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWorkPriority" runat="server" Text="Work Priority"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWorkPriority" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRWorkPriority" runat="server" ErrorMessage="Work Priority required."
                                ValidationGroup="entry" ControlToValidate="cboSRWorkPriority" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProblemDescription" runat="server" Text="Problem Description"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProblemDescription" runat="server" Width="300px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvProblemDescription" runat="server" ErrorMessage="Problem Description required."
                                ValidationGroup="entry" ControlToValidate="txtProblemDescription" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRequestByUserID" runat="server" Text="Request By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRequestByUserID" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWorkTrade" runat="server" Text="Work Order Trade"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWorkTrade" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboSRWorkTrade_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWordTradeItem" runat="server" Text="Work Order Trade Detail"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWorkTradeItem" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlAssetInfo">
        <table style="width: 100%" cellpadding="0" cellspacing="1">
            <tr>
                <td>
                    <fieldset>
                        <legend>ASSET INFORMATION</legend>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 50%; vertical-align: top">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">Asset
                                            </td>
                                            <td class="entry">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtAssetID" runat="server" Width="140px" AutoPostBack="true"
                                                                OnTextChanged="txtAssetID_TextChanged" />
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAssetName" runat="server" CssClass="labeldescription"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Item
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" AutoPostBack="False"
                                                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                                    OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                                        </b>
                                                        <br />
                                                        Item ID :
                                    <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Quantity
                                            </td>
                                            <td class="entry">
                                                <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="120px">
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Model Number
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtBrandName" runat="server" Width="300px" ReadOnly="True" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Serial Number
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtSerialNo" runat="server" Width="300px" ReadOnly="True" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblLocationName" runat="server" Text="Location"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtLocationName" runat="server" Width="300px" ReadOnly="True" />
                                            </td>
                                            <td width="20"></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">Asset Status
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSRAssetsStatus" Width="300px" Enabled="False">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Asset Notes
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtAssetNotes" runat="server" Width="300px" ReadOnly="True" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Notes To Technician
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtNotesToTechnician" runat="server" Width="300px" TextMode="MultiLine"
                                                    ReadOnly="True" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Warranty / Contract
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSRAssetsWarrantyContract" Width="300px"
                                                    Enabled="False">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">Warranty Expiry Date
                                            </td>
                                            <td class="entry">
                                                <telerik:RadDatePicker ID="txtGuaranteeExpiredDate" runat="server" Width="105px"
                                                    DateInput-ReadOnly="true" DatePopupButton-Enabled="false" />
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
    </asp:Panel>

    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Work Order Realization Information" PageViewID="pgWo"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Implementer Information" PageViewID="pgImp" />
            <telerik:RadTab runat="server" Text="Materials Used Information" PageViewID="pgMu" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgWo" runat="server" Selected="true">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRWorkStatus" runat="server" Text="Work Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRWorkStatus" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" OnItemDataBound="cboSRWorkStatus_ItemDataBound"
                                        OnItemsRequested="cboSRWorkStatus_ItemsRequested" Enabled="False">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                            </b>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRWorkStatus" runat="server" ErrorMessage="Work Status required."
                                        ValidationGroup="entry" ControlToValidate="cboSRWorkStatus" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRFailureCode" runat="server" Text="Failure Code"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRFailureCode" runat="server" Width="300px" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Cause Descriptions
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFailureCauseDescription" runat="server" Width="300px"
                                        MaxLength="500" TextMode="MultiLine" ReadOnly="True" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Action Taken
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtActionTaken" runat="server" Width="300px" MaxLength="500"
                                        TextMode="MultiLine" ReadOnly="True" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Prevention Taken
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPreventionTaken" runat="server" Width="300px" MaxLength="500"
                                        TextMode="MultiLine" ReadOnly="True" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr runat="server" id="trCostEstimation">
                                <td class="label">
                                    <asp:Label ID="lblCostEstimation" runat="server" Text="Cost Estimation"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtCostEstimation" runat="server" Width="100px" MaxLength="16"
                                        MinValue="0" ReadOnly="True" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAppovedDateTime" runat="server" Text="Created Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtAppovedDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                    DatePopupButton-Enabled="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtApprovedTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReceivedDateTime" runat="server" Text="Received Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtReceivedDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                                DatePopupButton-Enabled="false">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <telerik:RadMaskedTextBox ID="txtReceivedTime" runat="server" Mask="<00..23>:<00..59>"
                                                                PromptChar="_" RoundNumericRanges="false" Width="50px" Enabled="False">
                                                            </telerik:RadMaskedTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblReceivedByUserID" runat="server" Text="By" Width="30px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtReceivedBy" runat="server" Width="150px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label"></td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtReceivedByUserID" runat="server" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFirstResponseDateTime" runat="server" Text="First Response Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtFirstResponseDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                                DatePopupButton-Enabled="false">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <telerik:RadMaskedTextBox ID="txtFirstResponseTime" runat="server" Mask="<00..23>:<00..59>"
                                                                PromptChar="_" RoundNumericRanges="false" Width="50px" Enabled="False">
                                                            </telerik:RadMaskedTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblFirstResponseByUserID" runat="server" Text="By" Width="30px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtFirstResponseByUserName" runat="server" Width="150px"
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label"></td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFirstResponseByUserID" runat="server" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLastRealizationDateTime" runat="server" Text="Last Realization Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtLastRealizationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                                DatePopupButton-Enabled="false">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <telerik:RadMaskedTextBox ID="txtLastRealizationTime" runat="server" Mask="<00..23>:<00..59>"
                                                                PromptChar="_" RoundNumericRanges="false" Width="50px" Enabled="False">
                                                            </telerik:RadMaskedTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblLastRealizationByUserID" runat="server" Text="By" Width="30px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtLastRealizationBy" runat="server" Width="150px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label"></td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtLastRealizationByUserID" runat="server" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAcceptedDateTime" runat="server" Text="Closed Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtAcceptedDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                                DatePopupButton-Enabled="false">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <telerik:RadMaskedTextBox ID="txtAcceptedTime" runat="server" Mask="<00..23>:<00..59>"
                                                                PromptChar="_" RoundNumericRanges="false" Width="50px" Enabled="False">
                                                            </telerik:RadMaskedTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblAcceptedByUserID" runat="server" Text="By" Width="30px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtAcceptedByUserID" runat="server" Width="150px" ReadOnly="True" />
                                                        </td>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAcceptedBy" runat="server" Text="Accepted By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAcceptedBy" runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgImp" runat="server">
            <telerik:RadGrid ID="grdImplementer" runat="server" ShowFooter="false" OnNeedDataSource="grdImplementer_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="UserID" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="UserName" HeaderText="Implemented By"
                            UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgMu" runat="server">
            <telerik:RadGrid ID="grdItem" runat="server" ShowFooter="false" OnNeedDataSource="grdItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SeqNo" PageSize="10">
                    <Columns>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsMasterItem" HeaderText="Master"
                            UniqueName="IsMasterItem" SortExpression="IsMasterItem" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsInventoryItem"
                            HeaderText="Inventory" UniqueName="IsInventoryItem" SortExpression="IsInventoryItem"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="true" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                            HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60" DataField="Quantity" HeaderText="Qty"
                            UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60" DataField="QuantityRealization"
                            HeaderText="Qty Realization" UniqueName="QuantityRealization" SortExpression="Quantity"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
