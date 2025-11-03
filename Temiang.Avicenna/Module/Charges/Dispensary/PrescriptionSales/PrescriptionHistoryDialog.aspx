<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionHistoryDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionHistoryDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                OnItemsRequested="cboItemID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchItem" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Prescription Date" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />            
                                    </td>
                                    <td>
                                        &nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />            
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="10">
        <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="PrescriptionGroup" HeaderText="Prescription No ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="PrescriptionOrder" SortOrder="Descending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                    UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridDateTimeColumn DataField="PrescriptionDate" HeaderText="Presc. Date"
                    UniqueName="PrescriptionDate" SortExpression="PrescriptionDate">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                        <br />
                        <i>Intervention :</i>
                        <asp:Label ID="lblInterventionItemName" runat="server" Font-Italic="true" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemInterventionName")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="ResultQty" HeaderText="Qty" UniqueName="ResultQty"
                    SortExpression="ResultQty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                    HeaderStyle-Width="60px" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
