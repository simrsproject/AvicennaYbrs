<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SanitationControlSheetDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationControlSheetDetail" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblControlSheetNo" runat="server" Text="Control Sheet No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtControlSheetNo" runat="server" Width="160px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <asp:RequiredFieldValidator ID="rfvControlSheetNo" runat="server" ErrorMessage="No required."
                            ValidationGroup="entry" ControlToValidate="txtControlSheetNo" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblControlDate" runat="server" Text="Control Date/Time" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtControlDate" runat="server" Width="100px" AutoPostBack="true" 
                                            OnSelectedDateChanged="txtControlDate_SelectedDateChanged"/>
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtControlTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvControlDate" runat="server" ErrorMessage="Control Date required."
                                ValidationGroup="entry" ControlToValidate="txtControlDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQuestionForm" runat="server" Text="Control Sheet Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFormName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlSanitationControlSheetItem" runat="server" />
</asp:Content>