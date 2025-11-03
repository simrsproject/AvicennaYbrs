<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PhrPraRegDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PhrPraRegDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script src='<%=Page.ResolveUrl("~/JavaScript/Common/Core.js")%>' type="text/javascript"></script>

        <script type="text/javascript">
        </script>

    </telerik:RadCodeBlock>

    <asp:HiddenField runat="server" ID="hdnPatientID" />
    <asp:HiddenField runat="server" ID="hdnCreateByUserID" />
    <asp:HiddenField runat="server" ID="hdnQuestionFormID" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactioNo" runat="server" Text="No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="160px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRecordDate" runat="server" Text="Examination Time" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRecordDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtRecordTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRecordDate" runat="server" ErrorMessage="Vital Sign Date required."
                                ValidationGroup="entry" ControlToValidate="txtRecordDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQuestionForm" runat="server" Text="Form Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFormName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Form required."
                                ValidationGroup="entry" ControlToValidate="txtFormName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>

                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="30px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="20px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Sex" />
                        </td>
                        <td class="entry">
                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
                <telerik:RadButton runat="server" ID="btnCopyLast" Text="Copy Value From Last Document" SingleClickText="Process Copy Value From Last Document" SingleClick="True" OnClick="btnCopyLast_OnClick">
                    <ConfirmSettings ConfirmText="Are you sure you want to continue?" Height="200" Title="Copy Value From Last Document" Width="300" />
                </telerik:RadButton>
            </td>
        </tr>
    </table>

    <uc1:PhrCtl runat="server" ID="phrCtl" />
</asp:Content>
