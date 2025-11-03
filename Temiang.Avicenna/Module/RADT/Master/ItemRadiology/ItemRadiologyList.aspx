<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ItemRadiologyList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemRadiologyList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Group" UniqueName="ItemGroupName"
                    SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ReportRLID" HeaderText="Report RL" UniqueName="ReportRLID"
                    SortExpression="ReportRLID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsAdminCalculation"
                    HeaderText="Admin Calculation" UniqueName="IsAdminCalculation" SortExpression="IsAdminCalculation"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowVariable"
                    HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowCito" HeaderText="Cito"
                    UniqueName="IsAllowCito" SortExpression="IsAllowCito" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowDiscount"
                    HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPrintWithDoctorName"
                    HeaderText="Print Doctor" UniqueName="IsPrintWithDoctorName" SortExpression="IsPrintWithDoctorName"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAssetUtilization"
                    HeaderText="Asset" UniqueName="IsAssetUtilization" SortExpression="IsAssetUtilization"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
