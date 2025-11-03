<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="DocumentDefinitionList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DocumentDefinitionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="DocumentDefinitionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="DepartmentName" HeaderText="Department"
                    UniqueName="DepartmentName" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="NameFilesType" HeaderText="Files Analysis Type"
                    UniqueName="NameFilesType" SortExpression="NameFilesType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update Date Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Last Update By User ID" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="DocumentFilesID" Name="grdDetail" Width="100%"
                    AutoGenerateColumns="false" ShowFooter="false" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DocumentFilesID" HeaderText="ID"
                            UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DocumentNumber" HeaderText="Number"
                            UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DocumentName" HeaderText="Document Name" UniqueName="DocumentName"
                            SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
