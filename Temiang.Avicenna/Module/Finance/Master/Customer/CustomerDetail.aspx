<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="CustomerDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.CustomerDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCustomerID" runat="server" Text="Customer ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCustomerID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvCustomerID" runat="server" ErrorMessage="Customer ID required."
                                ValidationGroup="entry" ControlToValidate="txtCustomerID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCustomerName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ErrorMessage="Customer Name required."
                                ValidationGroup="entry" ControlToValidate="txtCustomerName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContactPerson" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ErrorMessage="Contact Person required."
                                ValidationGroup="entry" ControlToValidate="txtContactPerson" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Sales Margin Percentage"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSalesMarginPercentage" runat="server" Type="Percent"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdAR" runat="server" Text="COA (A/R Invoice)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAR" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdAR_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdAR_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdAR_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvChartOfAccountIdAR" runat="server" ErrorMessage="COA (A/R Invoice) required."
                                ValidationGroup="entry" ControlToValidate="cboChartOfAccountIdAR" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdAR" runat="server" Text="Subledger"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdAR" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdAR_ItemDataBound" OnItemsRequested="cboSubledgerIdAR_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountARTemporary" runat="server" Text="COA (A/R Process)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboChartOfAccountIdARTemporary" runat="server" Height="190px" Width="300px"
                                HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                OnItemsRequested="cboChartOfAccountIdARTemporary_ItemsRequested" OnItemDataBound="cboChartOfAccountIdARTemporary_ItemDataBound"
                                OnSelectedIndexChanged="cboChartOfAccountIdARTemporary_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvChartOfAccountIdARTemporary" runat="server" ErrorMessage="COA (A/R Process) required."
                                ValidationGroup="entry" ControlToValidate="cboChartOfAccountIdARTemporary" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerTemporary" runat="server" Text="Subledger"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdARTemporary" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdARTemporary_ItemDataBound" OnItemsRequested="cboSubledgerIdARTemporary_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Address" Selected="True" PageViewID="pgAddress">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgAddress" runat="server">
                        <uc1:Address ID="ctlAddress" runat="server" />
                    </telerik:RadPageView>

                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>
