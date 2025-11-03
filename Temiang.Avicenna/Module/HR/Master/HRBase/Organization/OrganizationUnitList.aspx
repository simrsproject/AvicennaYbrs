<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="OrganizationUnitList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.OrganizationUnitList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="OrganizationUnitID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OrganizationUnitID"
                    HeaderText="Organization Unit ID" UniqueName="OrganizationUnitID" SortExpression="OrganizationUnitID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OrganizationUnitCode"
                    HeaderText="Code" UniqueName="OrganizationUnitCode" SortExpression="OrganizationUnitCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderText="Organization Unit Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblOrganizationUnitName" runat="server" Text='<%# GetOrganizationUnitName(DataBinder.Eval(Container.DataItem, "SROrganizationLevel"), DataBinder.Eval(Container.DataItem, "OrganizationUnitName")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="ParentOrganizationUnitID"
                    HeaderText="Parent Organization Unit ID" UniqueName="ParentOrganizationUnitID"
                    SortExpression="ParentOrganizationUnitID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn DataField="ParentOrganizationUnitName"
                    HeaderText="Parent Organization Unit Name" UniqueName="ParentOrganizationUnitName" SortExpression="ParentOrganizationUnitName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="OrganizationLevelName"
                    HeaderText="Organization Level" UniqueName="OrganizationLevelName" SortExpression="OrganizationLevelName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="EmployeeName"
                    HeaderText="Lead By" UniqueName="EmployeeName" SortExpression="EmployeeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
