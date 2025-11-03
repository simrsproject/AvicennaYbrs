<%@ Page Title="Budgeting Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="BudgetingDetailDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.Budgeting.BudgetingDetailDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
</script>
    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnInsertCommand="grdList_InsertCommand" OnUpdateCommand="grdList_UpdateCommand" OnDeleteCommand="grdList_DeleteCommand" OnItemDataBound="grdList_ItemDataBound"
        AllowPaging="False" AllowSorting="true" ShowStatusBar="true" AutoGenerateColumns="false">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ItemID" ClientDataKeyNames="ItemID" >
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID">
                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsAsset" HeaderText="Asset" UniqueName="IsAsset" SortExpression="ItemName" />

                <telerik:GridBoundColumn DataField="Price" HeaderText="Price" UniqueName="Price" DataFormatString="{0:n2}"
                    SortExpression="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth01" HeaderText="Jan" UniqueName="QtyMonth01" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth01" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth02" HeaderText="Feb" UniqueName="QtyMonth02" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth02" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth03" HeaderText="Mar" UniqueName="QtyMonth03" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth03" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth04" HeaderText="Apr" UniqueName="QtyMonth04" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth04" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth05" HeaderText="May" UniqueName="QtyMonth05" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth05" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth06" HeaderText="Jun" UniqueName="QtyMonth06" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth06" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth07" HeaderText="Jul" UniqueName="QtyMonth07" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth07" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth08" HeaderText="Aug" UniqueName="QtyMonth08" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth08" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth09" HeaderText="Sep" UniqueName="QtyMonth09" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth09" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth10" HeaderText="Oct" UniqueName="QtyMonth10" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth10" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth11" HeaderText="Nov" UniqueName="QtyMonth11" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth11" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyMonth12" HeaderText="Dec" UniqueName="QtyMonth12" DataFormatString="{0:n2}"
                    SortExpression="QtyMonth12" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="Qty" HeaderText="Sum" UniqueName="Qty" DataFormatString="{0:n2}"
                    SortExpression="Qty" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Item Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Status" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%# (bool)DataBinder.Eval(Container.DataItem, "IsAssetApproved") ? "Approved" : ((bool)DataBinder.Eval(Container.DataItem, "IsAssetRejected") ? string.Format("Rejected, {0}", DataBinder.Eval(Container.DataItem, "RejectNotes")) :"") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="BudgetingDetailDialogEditor.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdListItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <br /><br /><br />
</asp:Content>