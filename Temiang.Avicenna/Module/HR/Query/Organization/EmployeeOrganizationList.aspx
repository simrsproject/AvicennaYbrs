<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeOrganizationList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.EmployeeOrganizationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmployeeOrganizationID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeOrganizationID"
                    HeaderText="Employee Organization ID" UniqueName="EmployeeOrganizationID" SortExpression="EmployeeOrganizationID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="OrganizationUnitCode"
                    HeaderText="Organization Code" UniqueName="OrganizationUnitCode" SortExpression="OrganizationUnitCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false"/>
                <telerik:GridNumericColumn HeaderStyle-Width="200px" DataField="Department"
                    HeaderText="Department" UniqueName="Department" SortExpression="Department"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                <telerik:GridNumericColumn HeaderStyle-Width="200px" DataField="Division"
                    HeaderText="Division" UniqueName="Division" SortExpression="Division"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                <telerik:GridNumericColumn HeaderStyle-Width="200px" DataField="SubDivision"
                    HeaderText="Sub Division" UniqueName="SubDivision" SortExpression="SubDivision"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                <telerik:GridNumericColumn HeaderStyle-Width="200px" DataField="Section"
                    HeaderText="Section" UniqueName="Section" SortExpression="Section"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
