<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CasemixExceptionDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.CasemixExceptionDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterItemService">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemProduct">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemProduct" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterItemProduct">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemProduct" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGuarantor" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table>
        <tr>
            <td class="label">Name
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtName" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name required."
                    ValidationGroup="entry" ControlToValidate="txtName" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
            <td />
        </tr>
        <tr>
            <td class="label">Notes
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr style="display:none">
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkActive" Text="Active" />
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="Guarantor" PageViewID="pgGuarantor" Selected="true" />
            <telerik:RadTab runat="server" Text="Item Service" PageViewID="pgItemService" />
            <telerik:RadTab runat="server" Text="Item Product" PageViewID="pgItemProduct" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgGuarantor" runat="server" Selected="true">
            <telerik:RadGrid ID="grdGuarantor" runat="server" OnNeedDataSource="grdGuarantor_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
                AllowSorting="true" OnInsertCommand="grdGuarantor_InsertCommand"
                OnDeleteCommand="grdGuarantor_DeleteCommand">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="CasemixCoveredID, GuarantorID" ClientDataKeyNames="CasemixCoveredID, GuarantorID"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn DataField="GuarantorID" HeaderText="Guarantor ID"
                            UniqueName="GuarantorID" SortExpression="GuarantorID">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name"
                            UniqueName="GuarantorName" SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CasemixExceptionGuarantorDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CasemixExceptionGuarantorDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgItemService" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemService" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemService" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemService" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
                AllowSorting="true" OnInsertCommand="grdItem_InsertCommand" OnUpdateCommand="grdItem_UpdateCommand"
                OnDeleteCommand="grdItem_DeleteCommand">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="CasemixCoveredID, ItemID" ClientDataKeyNames="CasemixCoveredID, ItemID"
                    GroupLoadMode="Client">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Global" Name="Global" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Inpatient" Name="Inpatient" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Outpatient" Name="Outpatient" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Emergency" Name="Emergency" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsUsingGlobalSetting" HeaderText="Using Global Setting"
                            UniqueName="IsUsingGlobalSetting" SortExpression="IsUsingGlobalSetting" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidate" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidate" SortExpression="IsNeedCasemixValidate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Global"/>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Quantity"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Global" />

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidateIpr" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidateIpr" SortExpression="IsNeedCasemixValidateIpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Inpatient"/>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyIpr" HeaderText="Quantity"
                            UniqueName="QtyIpr" SortExpression="QtyIpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Inpatient" />
                        
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidateOpr" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidateOpr" SortExpression="IsNeedCasemixValidateOpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Outpatient"/>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyOpr" HeaderText="Quantity"
                            UniqueName="QtyOpr" SortExpression="QtyOpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Outpatient" />
                        
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidateEmr" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidateEmr" SortExpression="IsNeedCasemixValidateEmr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Emergency"/>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyEmr" HeaderText="Quantity"
                            UniqueName="QtyEmr" SortExpression="QtyEmr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Emergency" />
                        <telerik:GridTemplateColumn />
                        
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CasemixExceptionItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CasemixExceptionItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgItemProduct" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemProduct" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemProduct" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemProduct" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdItemProduct" runat="server" OnNeedDataSource="grdItemProduct_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
                AllowSorting="true" OnInsertCommand="grdItemProduct_InsertCommand" OnUpdateCommand="grdItemProduct_UpdateCommand"
                OnDeleteCommand="grdItem_DeleteCommand">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="CasemixCoveredID, ItemID" ClientDataKeyNames="CasemixCoveredID, ItemID"
                    GroupLoadMode="Client">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Global" Name="Global" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Inpatient" Name="Inpatient" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Outpatient" Name="Outpatient" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Emergency" Name="Emergency" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsUsingGlobalSetting" HeaderText="Using Global Setting"
                            UniqueName="IsUsingGlobalSetting" SortExpression="IsUsingGlobalSetting" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidate" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidate" SortExpression="IsNeedCasemixValidate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Global"/>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsAllowedToOrder" HeaderText="Allowed To Order"
                            UniqueName="IsAllowedToOrder" SortExpression="IsAllowedToOrder" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Global"/>

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidateIpr" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidateIpr" SortExpression="IsNeedCasemixValidateIpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Inpatient"/>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsAllowedToOrderIpr" HeaderText="Allowed To Order"
                            UniqueName="IsAllowedToOrderIpr" SortExpression="IsAllowedToOrderIpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Inpatient"/>

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidateOpr" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidateOpr" SortExpression="IsNeedCasemixValidateOpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Outpatient"/>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsAllowedToOrderOpr" HeaderText="Allowed To Order"
                            UniqueName="IsAllowedToOrderOpr" SortExpression="IsAllowedToOrderOpr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Outpatient"/>

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsNeedCasemixValidateEmr" HeaderText="Need Casemix Validate"
                            UniqueName="IsNeedCasemixValidateEmr" SortExpression="IsNeedCasemixValidateEmr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Emergency"/>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsAllowedToOrderEmr" HeaderText="Allowed To Order"
                            UniqueName="IsAllowedToOrderEmr" SortExpression="IsAllowedToOrderEmr" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Emergency"/>
                        
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CasemixExceptionItemProductDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CasemixExceptionItemProductEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
