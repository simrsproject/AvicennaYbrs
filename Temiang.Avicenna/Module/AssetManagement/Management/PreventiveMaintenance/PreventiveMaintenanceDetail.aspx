<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PreventiveMaintenanceDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.PreventiveMaintenanceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPMNo" runat="server" Text="Prev. Maintenance No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPMNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPMNo" runat="server" ErrorMessage="Prev. Maintenance No required."
                                ValidationGroup="entry" ControlToValidate="txtPMNo" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPMDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPMDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPMDate" runat="server" ErrorMessage="Date required."
                                ValidationGroup="entry" ControlToValidate="txtPMDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Maintenance Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
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
                            <asp:Label ID="lblSRWorkTrade" runat="server" Text="Work Order Trade"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWorkTrade" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <%--<asp:RequiredFieldValidator ID="rfvSRWorkTrade" runat="server" ErrorMessage="Work Order Trade required."
                                ValidationGroup="entry" ControlToValidate="cboSRWorkTrade" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTargetDate" runat="server" Text="Target Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTargetDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTargetDate" runat="server" ErrorMessage="Target Date required."
                                ValidationGroup="entry" ControlToValidate="txtTargetDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label1" runat="server" Text="Asset / Location Information"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Asset
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtAssetID" runat="server" Width="120px" ReadOnly="True" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAssetName" runat="server" CssClass="labeldescription"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Serial Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSerialNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLocationName" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLocationName" runat="server" Width="300px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Visible="False" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Asset Status
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAssetsStatus" Width="300px" Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Asset Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssetNotes" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Notes To Technician
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotesToTechnician" runat="server" Width="300px" TextMode="MultiLine"
                                ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Warranty / Contract
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAssetsWarrantyContract" Width="300px"
                                Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Warranty Expiry Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtGuaranteeExpiredDate" runat="server" Width="105px"
                                DateInput-ReadOnly="true" DatePopupButton-Enabled="false" />
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
