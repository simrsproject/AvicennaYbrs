<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeClinicalPrivilegeDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeClinicalPrivilegeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeClinicalPrivilege" runat="server" ValidationGroup="EmployeeClinicalPrivilege" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeClinicalPrivilege"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnTransactionNo"/>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeClinicalPrivilegeID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeClinicalPrivilegeID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeClinicalPrivilegeID" runat="server" ErrorMessage="Employee Achievement ID required."
                            ControlToValidate="txtEmployeeClinicalPrivilegeID" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRProfessionGroup" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cboSRProfessionGroup_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRProfessionGroup" runat="server" ErrorMessage="Profession Group required."
                            ControlToValidate="cboSRProfessionGroup" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRClinicalWorkArea" runat="server" Text="Clinical Work Area"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRClinicalWorkArea" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboSRClinicalWorkArea_ItemDataBound"
                            OnItemsRequested="cboSRClinicalWorkArea_ItemsRequested" OnSelectedIndexChanged="cboSRClinicalWorkArea_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRClinicalWorkArea" runat="server" ErrorMessage="Clinical Work Area required."
                            ControlToValidate="cboSRClinicalWorkArea" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRClinicalAuthorityLevel" runat="server" Text="Clinical Authority Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRClinicalAuthorityLevel" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSRClinicalAuthorityLevel_ItemDataBound"
                            OnItemsRequested="cboSRClinicalAuthorityLevel_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRClinicalAuthorityLevel" runat="server" ErrorMessage="Clinical Authority Level required."
                            ControlToValidate="cboSRClinicalAuthorityLevel" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeClinicalPrivilege"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeClinicalPrivilege" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidDate" runat="server" Text="Valid From"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                                        MaxDate="12/31/2999" AutoPostBack="true" OnSelectedDateChanged="txtValidFrom_SelectedDateChanged"/>
                                </td>
                                <td>&nbsp;To</td>
                                <td>
                                    <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                                        MaxDate="12/31/2999" />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                            ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid Tp required."
                            ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDecreeNo" runat="server" Text="Decree No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDecreeNo" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDecreeNo" runat="server" ErrorMessage="Decree No required."
                            ControlToValidate="txtDecreeNo" SetFocusOnError="True" ValidationGroup="EmployeeClinicalPrivilege"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="80px" MaxLength="1000"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
