<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationGuarantorDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.RegistrationGuarantorDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRegistrationGuarantor" runat="server" ValidationGroup="RegistrationGuarantor" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RegistrationGuarantor"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cboGuarantorID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboGuarantorID" />
                <telerik:AjaxUpdatedControl ControlID="txtPlafondAmount" />
                <telerik:AjaxUpdatedControl ControlID="txtNotes" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                    </td>
                    <td class="entry">
                        <%--                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                            OnItemsRequested="cboGuarantorID_ItemsRequested" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged"
                            AutoPostBack="True">
                            <FooterTemplate>
                                Note : Show max 10 result
                            </FooterTemplate>
                        </telerik:RadComboBox>--%>

                        <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EmptyMessage="Select a Guarantor"
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                            OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged" AutoPostBack="True">
                            <WebServiceSettings Method="GuarantorsNotTypeSelf" Path="~/WebService/ComboBoxDataService.asmx" />
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                            ControlToValidate="cboGuarantorID" SetFocusOnError="True" ValidationGroup="RegistrationGuarantor"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPlafondAmount" runat="server" Text="Plafond Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPlafondAmount" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPlafondAmount" runat="server" ErrorMessage="Plafond required."
                            ControlToValidate="txtPlafondAmount" SetFocusOnError="True" ValidationGroup="RegistrationGuarantor"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RegistrationGuarantor"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RegistrationGuarantor" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

            </table>
        </td>
    </tr>
</table>
