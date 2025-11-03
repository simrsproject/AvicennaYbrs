<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="WashingProgramTypeList.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Master.WashingProgramTypeList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="LaundryProgramTypeID">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="LaundryProcessTypeName" HeaderText="Category "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="SRLaundryProcessType" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRLaundryProcessType" HeaderText="SRLaundryProcessType"
                    UniqueName="SRLaundryProcessType" SortExpression="SRLaundryProcessType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRLaundryProgram" HeaderText="SRLaundryProgram"
                    UniqueName="SRLaundryProgram" SortExpression="SRLaundryProgram" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="LaundryProgramName" HeaderText="Program Name" UniqueName="LaundryProgramName"
                    SortExpression="LaundryProgramName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRLaundryType" HeaderText="SRLaundryType"
                    UniqueName="SRLaundryType" SortExpression="SRLaundryType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="LaundryTypeName" HeaderText="Laundry Type" UniqueName="LaundryTypeName"
                    SortExpression="LaundryTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Weight" HeaderText="Weight (Kg)"
                    UniqueName="Weight" SortExpression="Weight" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>