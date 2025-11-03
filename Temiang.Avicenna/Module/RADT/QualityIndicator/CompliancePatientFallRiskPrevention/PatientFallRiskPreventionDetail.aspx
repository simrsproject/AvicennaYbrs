<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="PatientFallRiskPreventionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientFallRiskPreventionDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function cboRegistrationNo_ClientItemsRequesting(sender, eventArgs) {
                var regCombo = $find("<%= cboEmployeeID.ClientID %>");
                var regText = regCombo.get_value();
                var context = eventArgs.get_context();
                context["EmployeeNumber"] = regText;
            }
        </script>
    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" OnSelectedDateChanged="txtTransactionDate_SelectedDateChanged"
                                AutoPostBack="True">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblObserverID" runat="server" Text="Observer"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboObserverID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboObserverID_ItemDataBound"
                                OnItemsRequested="cboObserverID_ItemsRequested">
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
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvObserverID" runat="server" ErrorMessage="Observer required."
                                ValidationGroup="entry" ControlToValidate="cboObserverID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeID" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEmployeeID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                                OnItemsRequested="cboEmployeeID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboEmployeeID_SelectedIndexChanged">
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
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ErrorMessage="Employee Name required."
                                ValidationGroup="entry" ControlToValidate="cboEmployeeID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProfessionType" runat="server" Text="Profession Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProfessionType" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDepartmentID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboDepartmentID_ItemDataBound"
                                OnItemsRequested="cboDepartmentID_ItemsRequested" OnSelectedIndexChanged="cboDepartmentID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%>
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
                            <asp:Label ID="lblDivisionID" runat="server" Text="Division"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDivisionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboDivisionID_ItemDataBound"
                                OnItemsRequested="cboDivisionID_ItemsRequested" OnSelectedIndexChanged="cboDivisionID_SelectedIndexChanged">
                                <ItemTemplate>
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubDivisionID" runat="server" Text="Sub Division"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSubDivisionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboSubDivisionID_ItemDataBound"
                                OnItemsRequested="cboSubDivisionID_ItemsRequested" OnSelectedIndexChanged="cboSubDivisionID_SelectedIndexChanged">
                                <ItemTemplate>
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUnit" runat="server" Text="Section / Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboUnit" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboUnit_ItemDataBound"
                                OnItemsRequested="cboUnit_ItemsRequested">
                                <ItemTemplate>
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
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" AllowPaging="true" PageSize="20" runat="server"
    OnNeedDataSource="grdItem_NeedDataSource" AutoGenerateColumns="False"
    GridLines="None" OnUpdateCommand="grdItem_UpdateCommand" OnDeleteCommand="grdItem_DeleteCommand"
    OnInsertCommand="grdItem_InsertCommand">
    <HeaderContextMenu>
    </HeaderContextMenu>
    <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, RegistrationNo" AllowSorting="false">
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                <HeaderStyle Width="35px" />
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="RegistrationNo" HeaderText="Registration No"
                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left" />
            <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="MedicalNo" HeaderText="Medical No / Patient Name"
                UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left" />
            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRFallRiskStatus" HeaderText="Fall Risk Status"
                UniqueName="SRFallRiskStatus" SortExpression="SRFallRiskStatus" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left" />
            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRFallRiskPreventionEffort" HeaderText="Fall Risk Prevention Effort"
                UniqueName="SRFallRiskPreventionEffort" SortExpression="SRFallRiskPreventionEffort" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left" />
            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridDateTimeColumn>
            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                ButtonType="ImageButton" ConfirmText="Delete this row?">
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>
        </Columns>
        <EditFormSettings UserControlName="PatientFallRiskPreventionItem.ascx" EditFormType="WebUserControl">
            <EditColumn UniqueName="CompliancePatientFallRiskPreventionItemCommand">
            </EditColumn>
        </EditFormSettings>
    </MasterTableView>
    <FilterMenu>
    </FilterMenu>
    <ClientSettings EnableRowHoverStyle="true">
        <Resizing AllowColumnResize="True" />
        <Selecting AllowRowSelect="True" />
    </ClientSettings>
</telerik:RadGrid>
</asp:Content>

