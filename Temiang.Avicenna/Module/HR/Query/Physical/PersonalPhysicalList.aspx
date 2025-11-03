<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PersonalPhysicalList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.PersonalPhysicalList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="PersonalPhysicalID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonalPhysicalID"
                    HeaderText="Personal Physical ID" UniqueName="PersonalPhysicalID" SortExpression="PersonalPhysicalID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PhysicalCharacteristicName"
                    HeaderText="Characteristic" UniqueName="PhysicalCharacteristicName"
                    SortExpression="PhysicalCharacteristicName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PhysicalValue" HeaderText="Value"
                    UniqueName="PhysicalValue" SortExpression="PhysicalValue" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="MeasurementCodeName"
                    HeaderText="Measurement" UniqueName="MeasurementCodeName" SortExpression="MeasurementCodeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
