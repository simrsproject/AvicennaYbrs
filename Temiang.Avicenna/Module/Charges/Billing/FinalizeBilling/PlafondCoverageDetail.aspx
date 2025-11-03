<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PlafondCoverageDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling.PlafondCoverageDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboClassID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPlafondHistory" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPlafondHistory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPlafondHistory" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>PLAFOND COVERAGE
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td width="50%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">Coverage Formula / Margin
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadNumericTextBox runat="server" ID="txtBpjsCoverageFormula" Width="150px"
                                                            Type="Percent" />
                                                        <telerik:RadNumericTextBox runat="server" ID="txtMarginValue" Width="146px" Type="Percent"
                                                            ReadOnly="True" />
                                                    </td>
                                                    <td width="20px" />
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">INA-CBG Grouper
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboBpjsPackageID" runat="server" Width="300px" AutoPostBack="False"
                                                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True"
                                                            OnItemDataBound="cboBpjsPackageID_ItemDataBound" OnItemsRequested="cboBpjsPackageID_ItemsRequested">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "PackageName") %>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 15 result
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px" />
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">Class Name
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboClassID" Width="300px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cboClassID_SelectedIndexChanged" />
                                                    </td>
                                                    <td width="20px" />
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">Coverage Amount
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadNumericTextBox runat="server" ID="txtCoverageAmount" Width="150px" />
                                                    </td>
                                                    <td width="20px" />
                                                    <td />
                                                </tr>
                                                <tr runat="server" id="trBtnAdd">
                                                    <td class="label" />
                                                    <td class="entry">
                                                        <asp:Button runat="server" ID="btnAdd" Text="Insert" OnClick="btnAdd_Click" />
                                                    </td>
                                                    <td width="20px" />
                                                    <td />
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="50%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="grdPlafondHistory" runat="server" OnNeedDataSource="grdPlafondHistory_NeedDataSource"
                                                            AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15"
                                                            OnItemCommand="grdPlafondHistory_ItemCommand" OnDeleteCommand="grdPlafondHistory_DeleteCommand"
                                                            AllowSorting="False">
                                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, ClassID">
                                                                <Columns>
                                                                    <telerik:GridButtonColumn UniqueName="EditColumn" Text="View" CommandName="View"
                                                                        ButtonType="ImageButton" ImageUrl="~/Images/Toolbar/edit16.png">
                                                                        <HeaderStyle Width="35px" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                                                                        SortExpression="ClassName" />
                                                                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="CoverageAmount" HeaderText="Coverage Amount"
                                                                        UniqueName="CoverageAmount" SortExpression="CoverageAmount" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                                    <telerik:GridBoundColumn DataField="LastCreateUserID" HeaderText="Update By" UniqueName="LastCreateUserID"
                                                                        SortExpression="LastCreateUserID" HeaderStyle-Width="100px" />
                                                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                                        <HeaderStyle Width="35px" />
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                    </telerik:GridButtonColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings EnableRowHoverStyle="true">
                                                                <Resizing AllowColumnResize="True" />
                                                                <Selecting AllowRowSelect="True" />
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>BPJS S.E.P.
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td width="50%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">BPJS SEP No
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" MaxLength="50" />
                                                    </td>
                                                    <td width="20px" />
                                                    <td />
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="50%" style="vertical-align: top"></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
