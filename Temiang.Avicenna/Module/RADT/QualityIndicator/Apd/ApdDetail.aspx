<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ApdDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.ApdDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        OnInsertCommand="grdItem_InsertCommand" OnUpdateCommand="grdItem_UpdateCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SRApdType">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Indication" Name="Indication" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Usage Time" Name="UsageTime" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Usage Technique" Name="UsageTechnique" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Release Technique" Name="ReleaseTechnique" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SRApdType" HeaderText="ID"
                    UniqueName="SRApdType" SortExpression="SRApdType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ApdTypeName" HeaderText="APD Type"
                    UniqueName="ApdTypeName" SortExpression="ApdTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsCorrectIndication" HeaderText="Correct"
                    UniqueName="IsCorrectIndication" SortExpression="IsCorrectIndication" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Indication"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsIncorrectIndication" HeaderText="Incorrect"
                    UniqueName="IsIncorrectIndication" SortExpression="IsIncorrectIndication" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Indication"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsCorrectUsageTime" HeaderText="Appropriate"
                    UniqueName="IsCorrectUsageTime" SortExpression="IsCorrectUsageTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="UsageTime"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsIncorrectUsageTime" HeaderText="Inappropriate"
                    UniqueName="IsIncorrectUsageTime" SortExpression="IsIncorrectUsageTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="UsageTime"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsCorrectUsageTechnique" HeaderText="Correct"
                    UniqueName="IsCorrectUsageTechnique" SortExpression="IsCorrectUsageTechnique" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="UsageTechnique"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsIncorrectUsageTechnique" HeaderText="Incorrect"
                    UniqueName="IsIncorrectUsageTechnique" SortExpression="IsIncorrectUsageTechnique" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="UsageTechnique"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsCorrectReleaseTechnique" HeaderText="Correct"
                    UniqueName="IsCorrectReleaseTechnique" SortExpression="IsCorrectReleaseTechnique" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="ReleaseTechnique"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsIncorrectReleaseTechnique" HeaderText="Incorrect"
                    UniqueName="IsIncorrectReleaseTechnique" SortExpression="IsIncorrectReleaseTechnique" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="ReleaseTechnique"/>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ApdItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ApdItemDetailEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
