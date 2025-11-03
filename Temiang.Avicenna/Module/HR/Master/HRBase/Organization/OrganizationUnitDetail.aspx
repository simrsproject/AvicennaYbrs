<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="OrganizationUnitDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.OrganizationUnitDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Organization Unit ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtOrganizationUnitID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOrganizationUnitID" runat="server" ErrorMessage="Organization Unit ID required."
                                ValidationGroup="entry" ControlToValidate="txtOrganizationUnitID"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrganizationUnitCode" runat="server" Text="Organization Unit Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrganizationUnitCode" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOrganizationUnitCode" runat="server" ErrorMessage="Organization Unit Code required."
                                ValidationGroup="entry" ControlToValidate="txtOrganizationUnitCode"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrganizationUnitName" runat="server" Text="Organization Unit Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrganizationUnitName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOrganizationUnitName" runat="server" ErrorMessage="* if this Organization Unit top level"
                                ValidationGroup="entry" ControlToValidate="txtOrganizationUnitName"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParentOrganizationUnitID" runat="server" Text="Parent Organization Unit Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParentOrganizationUnitID" runat="server" Width="300px"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                AutoPostBack="false" OnItemDataBound="cboParentOrganizationUnitID_ItemDataBound"
                                OnItemsRequested="cboParentOrganizationUnitID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSROrganizationLevel" runat="server" Text="Organization Level"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSROrganizationLevel" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSROrganizationLevel" runat="server" ErrorMessage="Organization Level required."
                                ValidationGroup="entry" ControlToValidate="cboSROrganizationLevel"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Lead By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSubledgerId">
                        <td class="label">
                            <asp:Label ID="lblSubledgerId" runat="server" Text="Subledger (Payroll)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerId" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboSubledgerId_ItemDataBound"
                                OnItemsRequested="cboSubledgerId_ItemsRequested" OnSelectedIndexChanged="cboSubledgerId_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trDirectCost">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDirectCost" runat="server" Text="Direct Cost" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>

