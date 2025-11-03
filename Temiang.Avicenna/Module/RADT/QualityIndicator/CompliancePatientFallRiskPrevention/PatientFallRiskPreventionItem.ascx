<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientFallRiskPreventionItem.ascx.cs" 
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientFallRiskPreventionItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPatientFallRiskPreventionItem" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="PatientFallRiskPreventionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PatientFallRiskPreventionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboRegistrationNo" Width="300px" EmptyMessage="Select Patient"
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                            OnClientItemsRequesting="cboRegistrationNo_ClientItemsRequesting">
                            <WebServiceSettings Method="PatientRegistrationByServiceUnitUsers" Path="~/WebService/ComboBoxDataService.asmx" />
                            <ClientItemTemplate>
                                <div>
                                    <ul class="details">
                                        <li class="bold"><span>#= Value #</span></li>
                                        <li class="small"><span>#= Text #</span></li>
                                    </ul>
                                </div>
                            </ClientItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRFallRiskStatus" runat="server" Text="Fall Risk Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRFallRiskStatus" runat="server" Width="300px" EmptyMessage="">
                            <Items>
                                <telerik:RadComboBoxItem Text="None" Value="None" />
                                <telerik:RadComboBoxItem Text="Mild" Value="Mild" />
                                <telerik:RadComboBoxItem Text="Moderate" Value="Moderate" />
                                <telerik:RadComboBoxItem Text="Severe" Value="Severe" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRFallRiskPreventionEffort" runat="server" Text="Fall Risk Prevention Effort"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRFallRiskPreventionEffort" runat="server" Width="300px" EmptyMessage="">
                            <Items>
                                <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                <telerik:RadComboBoxItem Text="No" Value="No" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PatientFallRiskPreventionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PatientFallRiskPreventionItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
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