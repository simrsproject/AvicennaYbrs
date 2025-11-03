<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="AwardDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.AwardDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblAwardID" runat="server" Text="Award ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAwardID" runat="server" Width="300px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAwardCode" runat="server" Text="Award Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAwardCode" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAwardCode" runat="server" ErrorMessage="Award Code required."
                                ValidationGroup="entry" ControlToValidate="txtAwardCode"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAwardName" runat="server" Text="Award Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAwardName" runat="server" Width="300px" MaxLength="400" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAwardName" runat="server" ErrorMessage="Award Name required."
                                ValidationGroup="entry" ControlToValidate="txtAwardName"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAwardCriteria" runat="server" Text="Award Criteria"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAwardCriteria" runat="server" Width="304px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRAwardCriteria" runat="server" ErrorMessage="Award Criteria required."
                                ValidationGroup="entry" ControlToValidate="cboSRAwardCriteria"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAwardType" runat="server" Text="Award Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAwardType" runat="server" Width="304px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRAwardType" runat="server" ErrorMessage="Award Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRAwardType"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                ValidationGroup="entry" ControlToValidate="txtValidFrom"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                                ValidationGroup="entry" ControlToValidate="txtValidTo"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAwardPrize" runat="server" Text="Award Prize"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAwardPrize" runat="server" Width="300px" Height="80px" MaxLength="200" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAwardPrize" runat="server" ErrorMessage="Award Prize required."
                                ValidationGroup="entry" ControlToValidate="txtAwardPrize"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" Height="80px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

