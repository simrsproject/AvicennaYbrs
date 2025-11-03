<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClinicalPathwayItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ClinicalPathwayItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">Header Name
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboHeaderName" Width="300px" />
                        <telerik:RadTextBox runat="server" ID="txtHeaderName" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Group Name
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemGroupName" Width="300px" />
                        <telerik:RadTextBox runat="server" ID="txtItemGroupName" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Activity Name
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtAssesmentID" Width="300px" AutoPostBack="true"
                            OnTextChanged="txtAssesmentID_TextChanged" />
                        <telerik:RadTextBox runat="server" ID="txtAssesmentName" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td witdh="20px">
                        <asp:RequiredFieldValidator ID="rfvAssesmentName" runat="server" ErrorMessage="Assesment Name required."
                            ControlToValidate="txtAssesmentName" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">Coverage Value (Detail)
                    </td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td class="label">Class I (Rp.)
                                </td>
                                <td class="label">Class II (Rp.)
                                </td>
                                <td class="label">Class III (Rp.)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox runat="server" ID="txtClass1" Width="100px" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox runat="server" ID="txtClass2" Width="100px" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox runat="server" ID="txtClass3" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsActive" Text="Active" />
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                       
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">Day 1
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay1" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Day 2
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay2" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Day 3
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay3" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Day 4
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay4" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Day 5
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay5" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Day 6
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay6" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Day 7
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDay7" Width="104px">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Critical" Value="01" />
                                <telerik:RadComboBoxItem Text="Optional" Value="02" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Notes
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>

            </table>
        </td>
    </tr>
</table>
