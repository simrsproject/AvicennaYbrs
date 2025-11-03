<%@ Page Title="Notes" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="BillingAdjustSettingDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Charges.BillingAdjustSettingDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
                    Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Item Setting" PageViewID="pg1"
                            Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Item Group Setting" PageViewID="pg2">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
                    <telerik:RadPageView ID="pg1" runat="server" Selected="true">
                        <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grid_NeedDataSource"
                            OnPreRender="grid_PreRender"  AutoGenerateColumns="False"
                            GridLines="None" OnInsertCommand="grid_InsertCommand" 
                            OnUpdateCommand="grid_UpdateCommand" OnDeleteCommand="grid_DeleteCommand" >
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="SpecialtyName" HeaderText="Specialty" UniqueName="SpecialtyName"
                                        SortExpression="SpecialtyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="RegistrationTypeName" HeaderText="Registration Type" UniqueName="RegistrationTypeName"
                                        SortExpression="RegistrationTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    
                                    <telerik:GridBoundColumn DataField="TariffTypeName" HeaderText="Tarif Name" UniqueName="TariffTypeName"
                                        SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                                        SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                                        SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tarif Component Name" UniqueName="TariffComponentName"
                                        SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridCheckBoxColumn DataField="IsFeeValueInPercent" HeaderText="Is Value In Percent" UniqueName="IsFeeValueInPercent"
                                        SortExpression="IsFeeValueInPercent" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridCheckBoxColumn>
                                    
                                    <telerik:GridBoundColumn DataField="FeeValue" HeaderText="Fee Value" UniqueName="FeeValue"
                                        SortExpression="FeeValue" DataFormatString="{0:n2}" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="colFeeValue" HeaderText="Fee Value" HeaderStyle-Width="90">
                                        <ItemTemplate>
                                            <telerik:RadTextBox ID="txtFeeValue" runat="server" Width="70" ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridBoundColumn DataField="ItemGroupReplacementName" HeaderText="Item Group Replacement" UniqueName="ItemGroupReplacementName"
                                        SortExpression="ItemGroupReplacementName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="25px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="BillingAdjustSettingDetail.ascx" EditFormType="WebUserControl">
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pg2" runat="server">
                        <telerik:RadGrid ID="grdItemGroup" runat="server" OnNeedDataSource="grdItemGroup_NeedDataSource"
                            OnItemDataBound="grdItemGroup_ItemDataBound"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemGroupID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group" UniqueName="ItemGroupName"
                                        SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn UniqueName="disc" HeaderStyle-Width="50px" HeaderText="%">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtDisc" runat="server"
                                                Width="30px" IncrementSettings-InterceptMouseWheel="false">
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblDiscSelection" runat="server" BorderStyle="None" 
                                                CellPadding="0" CellSpacing="0" BorderWidth="0" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Disc Tarif" Value="0" Selected="True" />
                                                <asp:ListItem Text="Grouping" Value="1" />
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>