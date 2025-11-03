<%@ Page Title="Guarantor Info" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="GuarantorInfoDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorInfoDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address2" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function UpdateInformationCount(objectName, iCount) {
                //alert(objectName);
                if (objectName == null || objectName == undefined || objectName == 'none') {
                    // do nothing
                } else {
                    var obj = GetRadWindow().BrowserWindow.document.getElementById(objectName);
                    obj.innerHTML = iCount
                    if (iCount > 0) {
                        // set bubble visible true
                        obj.style.visibility = 'visible';
                    } else {
                        // set bubble visible false
                        obj.style.visibility = 'hidden';
                    }
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function CallFnOnParent() {
                GetRadWindow().BrowserWindow.CalledFn();
            }
        </script>

        <style type="text/css">
            .MyImageButton
            {
                cursor: hand;
            }

            .EditFormHeader td
            {
                font-size: 14px;
                padding: 4px !important;
                color: #0066cc;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridMaster" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <telerik:RadTabStrip ID="tabInfo" runat="server" MultiPageID="mpgInfo" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="General Information" PageViewID="pgvGeneral"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Notes" PageViewID="pgvInfo">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgInfo" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvGeneral" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblHeader" runat="server" Text="Guarantor Header" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorIdHd" runat="server" Text="Guarantor ID"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtGuarantorIdHd" runat="server" Width="100px" MaxLength="10"
                                                    ReadOnly="True" />
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActiveHd" runat="server" Text="Active" Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorNameHd" runat="server" Text="Guarantor Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtGuarantorNameHd" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="mpgInfoDetailHD" SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="General Information" PageViewID="rpvInfoDetailHD"
                                                Selected="true">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Contract Summary" PageViewID="rpvContractSumHD">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Related Companies" PageViewID="rpvRelatedCompsHD">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="mpgInfoDetailHD" runat="server" SelectedIndex="0" BorderStyle="Solid"
                                        BorderColor="gray">
                                        <telerik:RadPageView ID="rpvInfoDetailHD" runat="server">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblContractStartHd" runat="server" Text="Contract Start & End"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtContractStartHd" runat="server" Width="100px" Enabled="False" />
                                                                </td>
                                                                <td>&nbsp;To&nbsp;
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtContractEndHd" runat="server" Width="100px" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblContactPersonHd" runat="server" Text="Contact Person"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtContactPersonHd" runat="server" Width="300px" ReadOnly="True" 
                                                            Height="70px" TextMode="MultiLine" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblGuarantorTypeHd" runat="server" Text="Guarantor Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRGuarantorTypeHd" runat="server" Width="300px" Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblBusinessMethodHd" runat="server" Text="Business Method"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRBusinessMethodHd" runat="server" Width="300px" Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblTariffTypeHd" runat="server" Text="Tariff Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRTariffTypeHd" runat="server" Width="300px" Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr height="24px">
                                                    <td class="label">
                                                        <asp:Label ID="lblRegistrationTypeCoveredHd" runat="server" Text="Cover Registration Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsCoverInpatientHd" runat="server" Text="Inpatient" Enabled="False" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsCoverOutpatientHd" runat="server" Text="Outpatient" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="Label9" runat="server" Text="Cover Item Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsIncludeItemMedicalHd" runat="server" Text="Medical" Enabled="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblToGuarantorMedicalHd" runat="server" RepeatDirection="Horizontal"
                                                                        Enabled="False">
                                                                        <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                                        <asp:ListItem>To Patient</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsIncludeItemNonMedicalHd" runat="server" Text="Non Medical"
                                                                        Enabled="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblToGuarantorNonMedicalHd" runat="server" RepeatDirection="Horizontal"
                                                                        Enabled="False">
                                                                        <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                                        <asp:ListItem>To Patient</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr height="24px">
                                                    <td class="label"></td>
                                                    <td class="entry">
                                                        <asp:CheckBox ID="chkIsIncludeAdminValueHd" runat="server" Text="Cover Administration"
                                                            Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblAdminInpatientHd" runat="server" Text="Inpatient"></asp:Label>
                                                                </td>
                                                                <td class="label">
                                                                    <asp:Label ID="lblAdminOutpatientHd" runat="server" Text="Outpatient"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAdminPercentageHd" runat="server" Text="Admin Percentage"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminPercentageHd" runat="server" Type="Percent"
                                                                        MinValue="0" ReadOnly="True" />
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminPercentageOpHd" runat="server" Type="Percent"
                                                                        MinValue="0" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAdminAmountLimitHd" runat="server" Text="Admin Amount Limit (Min)"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMinHd" runat="server" MinValue="0" ReadOnly="True" />
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMinOpHd" runat="server" MinValue="0"
                                                                        ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAdminAmountMaxHd" runat="server" Text="Admin Amount Limit (Max)"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMaxHd" runat="server" MinValue="0" ReadOnly="True" />
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMaxOpHd" runat="server" MinValue="0"
                                                                        ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="rpvContractSumHD" runat="server">
                                            <telerik:RadTextBox ID="txtContractSummaryHd" runat="server" Width="100%" TextMode="MultiLine"
                                                Height="400px" ReadOnly="True" />
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="rpvRelatedCompsHD" runat="server">
                                            <telerik:RadTextBox ID="txtNoteRelatedCompaniesHd" runat="server" Width="100%" TextMode="MultiLine"
                                                Height="400px" ReadOnly="True" />
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblDetail" runat="server" Text="Guarantor Detail" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor ID"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" MaxLength="10"
                                                    ReadOnly="True" />
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorName" runat="server" Text="Guarantor Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <telerik:RadTabStrip ID="RadTabStrip3" runat="server" MultiPageID="mpgInfoDetailDT" SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="General Information" PageViewID="rpvInfoDetailDT"
                                                Selected="true">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Contract Summary" PageViewID="rpvContractSumDT">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Related Companies" PageViewID="rpvRelatedCompsDT">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="mpgInfoDetailDT" runat="server" SelectedIndex="0" BorderStyle="Solid"
                                        BorderColor="gray">
                                        <telerik:RadPageView ID="rpvInfoDetailDT" runat="server">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblContractStart" runat="server" Text="Contract Start & End"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtContractStart" runat="server" Width="100px" Enabled="False" />
                                                                </td>
                                                                <td>&nbsp;To&nbsp;
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtContractEnd" runat="server" Width="100px" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtContactPerson" runat="server" Width="300px" ReadOnly="True" 
                                                            Height="70px" TextMode="MultiLine" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblGuarantorType" runat="server" Text="Guarantor Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRGuarantorType" runat="server" Width="300px" Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblBusinessMethod" runat="server" Text="Business Method"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRBusinessMethod" runat="server" Width="300px" Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblTariffType" runat="server" Text="Tariff Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRTariffType" runat="server" Width="300px" Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr height="24px">
                                                    <td class="label">
                                                        <asp:Label ID="lblRegistrationTypeCovered" runat="server" Text="Cover Registration Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsCoverInpatient" runat="server" Text="Inpatient" Enabled="False" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsCoverOutpatient" runat="server" Text="Outpatient" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblIsIncludeItem" runat="server" Text="Cover Item Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsIncludeItemMedical" runat="server" Text="Medical" Enabled="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblToGuarantorMedical" runat="server" RepeatDirection="Horizontal"
                                                                        Enabled="False">
                                                                        <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                                        <asp:ListItem>To Patient</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsIncludeItemNonMedical" runat="server" Text="Non Medical" Enabled="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblToGuarantorNonMedical" runat="server" RepeatDirection="Horizontal"
                                                                        Enabled="False">
                                                                        <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                                        <asp:ListItem>To Patient</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr height="24px">
                                                    <td class="label"></td>
                                                    <td class="entry">
                                                        <asp:CheckBox ID="chkIsIncludeAdminValue" runat="server" Text="Cover Administration"
                                                            Enabled="False" />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblAdminInpatient" runat="server" Text="Inpatient"></asp:Label>
                                                                </td>
                                                                <td class="label">
                                                                    <asp:Label ID="lblAdminOutpatient" runat="server" Text="Outpatient"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAdminPercentage" runat="server" Text="Admin Percentage"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminPercentage" runat="server" Type="Percent"
                                                                        MinValue="0" ReadOnly="True" />
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminPercentageOp" runat="server" Type="Percent"
                                                                        MinValue="0" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAdminAmountLimit" runat="server" Text="Admin Amount Limit (Min)"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMin" runat="server" MinValue="0" ReadOnly="True" />
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMinOp" runat="server" MinValue="0" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAdminAmountMax" runat="server" Text="Admin Amount Limit (Max)"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMax" runat="server" MinValue="0" ReadOnly="True" />
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtAdminAmountMaxOp" runat="server" MinValue="0" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="rpvContractSumDT" runat="server">
                                            <telerik:RadTextBox ID="txtContractSummary" runat="server" Width="100%" TextMode="MultiLine"
                                                Height="400px" ReadOnly="True" />
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="rpvRelatedCompsDT" runat="server">
                                            <telerik:RadTextBox ID="txtNoteRelatedCompanies" runat="server" Width="100%" TextMode="MultiLine"
                                                Height="400px" ReadOnly="True" />
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="Guarantor Header" Selected="True" PageViewID="pgAddress">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Guarantor Detail" PageViewID="pgAddress2">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
                            BorderColor="Gray">
                            <telerik:RadPageView ID="pgAddress" runat="server">
                                <uc1:Address ID="ctlAddress" runat="server" />
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pgAddress2" runat="server">
                                <uc1:Address2 ID="ctlAddress2" runat="server" />
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvInfo" runat="server">
            <asp:Panel ID="PanelGrid" runat="server" Height="390px" ScrollBars="Vertical">
                <telerik:RadGrid ID="RadGridMaster" GridLines="None" runat="server" AllowAutomaticDeletes="False"
                    AllowAutomaticInserts="False" PageSize="10" AllowAutomaticUpdates="False" AllowPaging="True"
                    AutoGenerateColumns="False" OnItemUpdated="RadGridMaster_ItemUpdated" OnItemDeleted="RadGridMaster_ItemDeleted"
                    OnItemInserted="RadGridMaster_ItemInserted" OnDataBound="RadGridMaster_DataBound"
                    OnNeedDataSource="RadGridMaster_NeedDataSource" OnInsertCommand="RadGridMaster_InsertCommand"
                    OnUpdateCommand="RadGridMaster_UpdateCommand" OnDeleteCommand="RadGridMaster_DeleteCommand">
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="GuarantorInfoID"
                        HorizontalAlign="NotSet" AutoGenerateColumns="False">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="Information" HeaderText="Information" UniqueName="Information"
                                ColumnEditorID="GridTextBoxColumnEditorInformation">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="User" UniqueName="UserName"
                                ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update" UniqueName="LastUpdateDateTime"
                                SortExpression="LastUpdateDateTime" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridButtonColumn ConfirmText="Delete this data?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                UniqueName="DeleteColumn">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings ColumnNumber="1" CaptionDataField="GuarantorInfoID" CaptionFormatString="Edit properties of ID: {0}">
                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                                Width="100%" />
                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                            <EditColumn ButtonType="ImageButton" InsertText="Insert Order" UpdateText="Update record"
                                UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                            </EditColumn>
                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
                <telerik:GridTextBoxColumnEditor ID="GridTextBoxColumnEditorInformation" runat="server"
                    TextBoxStyle-Width="450px" TextBoxMode="MultiLine" />
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
