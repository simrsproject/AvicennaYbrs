<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemLaboratoryResult.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ItemLaboratoryResult" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        Sex
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSex" Width="300px">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <%--<telerik:RadComboBoxItem Value="A" Text="All" />--%>
                                <telerik:RadComboBoxItem Value="F" Text="Female" />
                                <telerik:RadComboBoxItem Value="M" Text="Male" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sex required."
                            ControlToValidate="cboSex" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Age Type
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRAgeType" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Age Type required."
                            ControlToValidate="cboSRAgeType" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Age Min
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox runat="server" ID="txtAgeMin" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Age Min required."
                            ControlToValidate="txtAgeMin" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Age Max
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox runat="server" ID="txtAgeMax" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Age Max required."
                            ControlToValidate="txtAgeMax" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Value Type
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRanswerType" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboSRanswerType_SelectedIndexChanged" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Value Reference
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboValueReference" Width="300px" AllowCustomText="true"
                            Filter="Contains" Enabled="false" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        Normal Value Min
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNormalValueMin" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Normal Value Max
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNormalValueMax" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Notes
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
