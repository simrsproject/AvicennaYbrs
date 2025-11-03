<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" 
	CodeBehind="RenkinList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.RenkinList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterPosition">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdlist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdlist">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdlist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <%--Search Filter--%>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title ="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" Filter="Contains" OnItemDataBound="cboPositionID_ItemDataBound"
                                    OnItemsRequested="cboPositionID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 15 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterPositionID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Position" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="RenkinID">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="PositionName" HeaderText="Posisi "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="PositionID" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="RenkinID" HeaderText="Renkin ID" UniqueName="RenkinID" SortExpression="RenkinID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Kegiatan" HeaderText="Kegiatan" UniqueName="Kegiatan" SortExpression="Kegiatan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PositionName" HeaderText="Posisi" UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Renkin Jenis Kegiatan" UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TargetPersen" HeaderText="Target Persen" UniqueName="TargetPersen" SortExpression="TargetPersen" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TargetBulan" HeaderText="Target Bulan" UniqueName="TargetBulan" SortExpression="TargetBulan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
    </telerik:RadGrid>
</asp:Content>

