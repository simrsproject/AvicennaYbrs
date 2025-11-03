<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="TestResultTemplateDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.TestResultTemplateDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%;vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTestResultTemplateID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTestResultTemplateID" runat="server" Width="100px" ReadOnly="true" />
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
                            <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                                    &nbsp;(<b><%# DataBinder.Eval(Container.DataItem, "ParamedicID")%></b>)
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
                </table>
            </td>
            <td style="width: 50%;vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item*"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    &nbsp;(<b><%# DataBinder.Eval(Container.DataItem, "ItemID")%></b>)
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
                            <asp:Label ID="lblTestResultTemplateName" runat="server" Text="Template Name*"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTestResultTemplateName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTestResultTemplateName" runat="server" ErrorMessage="TestResultTemplate Name required."
                                ValidationGroup="entry" ControlToValidate="txtTestResultTemplateName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="50%" class="labelcaption">
                <asp:Label runat="server" ID="lblNative" Text="Native Language" Font-Bold="True" />
            </td>
            <td width="50%" class="labelcaption">
                <asp:Label runat="server" ID="lblOther" Text="Other Language" Font-Bold="True" />
            </td>
        </tr>
        <tr>
            <td width="50%">
                <telerik:RadEditor ID="txtTestResult" runat="server" Width="100%" Height="500px" />
            </td>
            <td width="50%">
                <telerik:RadEditor ID="txtTestResultOtherLang" runat="server" Width="100%" Height="500px" />
            </td>
        </tr>
    </table>
</asp:Content>
