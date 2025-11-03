<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="DietDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.DietDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDietID" runat="server" Text="Diet ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDietID" runat="server" Width="100px" MaxLength="20">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDietID" runat="server" ErrorMessage="Diet ID required"
                    ValidationGroup="entry" ControlToValidate="txtDietID" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDietName" runat="server" Text="Diet Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDietName" runat="server" Width="300px" MaxLength="200" TextMode="MultiLine">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDietName" runat="server" ErrorMessage="Diet Name required"
                    ValidationGroup="entry" ControlToValidate="txtDietName" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRDietType" runat="server" Text="Diet Category"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRDietType" runat="server" Width="300px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSRDietType" runat="server" ErrorMessage="Diet Category required"
                    ValidationGroup="entry" ControlToValidate="cboSRDietType" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblPriorityNo" runat="server" Text="Priority No"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <telerik:RadNumericTextBox ID="txtPriorityNo" runat="server" Width="100px" MaxLength="10"
                    MinValue="0" NumberFormat-DecimalDigits="0" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="entry" colspan="3">
                <table width="100%">
                    <tr>
                        <td style="width: 10%" class="labelcaption">
                            <b>
                                <asp:Label ID="Label2" runat="server" Text="Standard"></asp:Label></b>
                        </td>
                        <td style="width: 20%" class="labelcaption">
                            <b>
                                <asp:Label ID="Label3" runat="server" Text="Range"></asp:Label></b>
                        </td>
                        <td style="width: 10%" class="labelcaption">
                            <b>
                                <asp:Label ID="Label4" runat="server" Text="Interval"></asp:Label></b>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCalorie" runat="server" Text="Calorie"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtCalorie" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtCalorieMin" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            &nbsp;to&nbsp;
                            <telerik:RadNumericTextBox ID="txtCalorieMax" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtCalorieInterval" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblProtein" runat="server" Text="Protein"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtProtein" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtProteinMin" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            &nbsp;to&nbsp;
                            <telerik:RadNumericTextBox ID="txtProteinMax" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtProteinInterval" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFat" runat="server" Text="Fat"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtFat" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtFatMin" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            &nbsp;to&nbsp;
                            <telerik:RadNumericTextBox ID="txtFatMax" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtFatInterval" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCarbohydrate" runat="server" Text="Carbohydrate"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtCarbohydrate" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtCarbohydrateMin" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            &nbsp;to&nbsp;
                            <telerik:RadNumericTextBox ID="txtCarbohydrateMax" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtCarbohydrateInterval" runat="server" Width="100px"
                                MaxLength="10" MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSalt" runat="server" Text="Salt"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtSalt" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtSaltMin" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            &nbsp;to&nbsp;
                            <telerik:RadNumericTextBox ID="txtSaltMax" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtSaltInterval" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFiber" runat="server" Text="Fiber"></asp:Label>
            </td>
            <td class="entry" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtFiber" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtFiberMin" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            &nbsp;to&nbsp;
                            <telerik:RadNumericTextBox ID="txtFiberMax" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td style="width: 10%">
                            <telerik:RadNumericTextBox ID="txtFiberInterval" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 22px">
            </td>
            <td class="entry" style="height: 22px">
                <asp:CheckBox ID="chkIsGetSnack" Text="Get Snack" runat="server" />
            </td>
            <td width="20" style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 22px">
            </td>
            <td class="entry" style="height: 22px">
                <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
            </td>
            <td width="20" style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Diet Menu" PageViewID="pgvMenu" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Diet Complication" PageViewID="pgvComplication">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvMenu" runat="server">
            <telerik:RadGrid ID="grdDietMenu" runat="server" OnNeedDataSource="grdDietMenu_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDietMenu_UpdateCommand"
                OnInsertCommand="grdDietMenu_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="FormOfFood">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="FormOfFood"
                            HeaderText="FormOfFood" UniqueName="FormOfFood" SortExpression="FormOfFood" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FormOfFoodName"
                            HeaderText="Form Of Food" UniqueName="FormOfFoodName" SortExpression="FormOfFoodName" />    
                        <telerik:GridBoundColumn DataField="MenuName" HeaderText="Menu Name"
                            UniqueName="MenuName" SortExpression="MenuName" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <EditFormSettings UserControlName="DietMenuDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="DietMenuEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings>
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvComplication" runat="server">
            <telerik:RadGrid ID="grdDietComplication" runat="server" OnNeedDataSource="grdDietComplication_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDietComplication_UpdateCommand"
                OnInsertCommand="grdDietComplication_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="DietComplicationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DietComplicationID"
                            HeaderText="Diet Complication ID" UniqueName="DietComplicationID" SortExpression="DietComplicationID" />
                        <telerik:GridBoundColumn DataField="DietComplicationName" HeaderText="Diet Complication Name"
                            UniqueName="DietComplicationName" SortExpression="DietComplicationName" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <EditFormSettings UserControlName="DietComplicationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="DietComplicationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings>
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
