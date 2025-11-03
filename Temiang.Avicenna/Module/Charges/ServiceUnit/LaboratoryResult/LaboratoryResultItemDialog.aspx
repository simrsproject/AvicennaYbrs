<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="LaboratoryResultItemDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.LaboratoryResultItemDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdLabResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLabResult" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Item Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="304px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Item Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" ReadOnly="true" Width="300px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Laboratory Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRLabUnit" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Normal Value" PageViewID="pgvConsumption" Selected="true" />
            <telerik:RadTab runat="server" Text="Profile" PageViewID="pgvPriceHistory" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvConsumption" runat="server">
            <telerik:RadGrid ID="grdLabResult" runat="server" OnNeedDataSource="grdLabResult_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdLabResult_DeleteCommand"
                OnInsertCommand="grdLabResult_InsertCommand" OnUpdateCommand="grdLabResult_UpdateCommand">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="ItemID, SequenceNo" AllowPaging="true"
                    PageSize="10">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Sex"
                            UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AgeUnitName" HeaderText="Age Unit"
                            UniqueName="AgeUnitName" SortExpression="AgeUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AgeMin" HeaderText="Age Min"
                            UniqueName="AgeMin" SortExpression="AgeMin" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AgeMax" HeaderText="Age Max"
                            UniqueName="AgeMax" SortExpression="AgeMax" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="AnswerTypeName" HeaderText="Value Type" UniqueName="AnswerTypeName"
                            SortExpression="AnswerTypeName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="AnswerTypeReferenceName" HeaderText="Value Reference"
                            UniqueName="AnswerTypeReferenceName" SortExpression="AnswerTypeReferenceName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NormalValueMin" HeaderText="Normal Value Min"
                            UniqueName="NormalValueMin" SortExpression="NormalValueMin" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NormalValueMax" HeaderText="Normal Value Max"
                            UniqueName="NormalValueMax" SortExpression="NormalValueMax" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="../../../RADT/Master/ItemLaboratory/ItemLaboratoryResult.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemLabResultEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvPriceHistory" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" GroupLoadMode="Client"
                OnNeedDataSource="grdList_NeedDataSource" OnDetailTableDataBind="grdList_DetailTableDataBind"
                OnItemCommand="grdList_ItemCommand">
                <MasterTableView DataKeyNames="ParentItemID,DetailItemID,DisplaySequence">
                    <SortExpressions>
                        <telerik:GridSortExpression FieldName="DisplaySequence" SortOrder="Ascending" />
                    </SortExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnOrder" HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnOrder" CommandName="order" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParentItemID") + "|" + DataBinder.Eval(Container.DataItem, "DetailItemID") + "|0"%>'>
                                    <img alt="" src="../../../../Images/Toolbar/arrowup_blue16.png" border="0" title="Order" />    
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Detail Item ID"
                            UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DetailItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                            SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="ParentItemID,DetailItemID,DisplaySequence"
                            AutoGenerateColumns="false" GroupLoadMode="Client">
                            <SortExpressions>
                                <telerik:GridSortExpression FieldName="DisplaySequence" SortOrder="Ascending" />
                            </SortExpressions>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumnOrder" HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnOrder" CommandName="order" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParentItemID") + "|" + DataBinder.Eval(Container.DataItem, "DetailItemID") + "|1"%>'>
                                            <img alt="" src="../../../../Images/Toolbar/arrowup_blue16.png" border="0" title="Order" />    
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Detail Item ID"
                                    UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="DetailItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                                    SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
