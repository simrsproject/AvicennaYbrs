<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="TestResultAllParamedicDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.TestResultAllParamedicDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="TransactionNo"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClinicalInfo" runat="server" Text="Clinical Information"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClinicalInfo" runat="server" Width="300px" TextMode="MultiLine" />
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
                            Test Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="true" />
                            <asp:Label ID="lblItemName" runat="server" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTestResultTemplateID" runat="server" Text="Result Template"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboTestResultTemplateID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboTestResultTemplateID_ItemDataBound"
                                OnItemsRequested="cboTestResultTemplateID_ItemsRequested" OnSelectedIndexChanged="cboTestResultTemplateID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "TestResultTemplateName")%>
                                    &nbsp;(<b><%# DataBinder.Eval(Container.DataItem, "TestResultTemplateID")%></b>)
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Physician Sender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboPhysicianSender" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="false" AutoPostBack="false" AllowCustomText="true" OnItemDataBound="cboPhysicianSender_ItemDataBound"
                                OnItemsRequested="cboPhysicianSender_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEntryDateTime" runat="server" Text="Entry Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEntryDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtEntryTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="true">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEntryDate" runat="server" ErrorMessage="Entry Date required."
                                ValidationGroup="entry" ControlToValidate="txtEntryDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblS" Text="(S) Subjective" />
                        </td>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblO" Text="(O) Objective" />
                        </td>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblA" Text="(A) Assessment" />
                        </td>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblP" Text="(P) Planning" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtS" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtO" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtA" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtP" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Native Language" PageViewID="pgNative" Selected="True" />
            <telerik:RadTab runat="server" Text="Foreign Language" PageViewID="pgOther" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgNative" runat="server" Selected="true">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblResult" runat="server" Text="Result*"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestResult" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSummary" runat="server" Text="Summary"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSummary" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSuggest" runat="server" Text="Suggest"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSuggest" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOther" runat="server">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Result"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestResultOtherLang" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Summary"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSummaryOtherLang" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="Label5" runat="server" Text="Suggest"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSuggestOtherLang" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
