<%@ Page Title="Working Hour" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ScheduleDetailDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.ScheduleDetailDialog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="WorkingScheduleID, WorkingScheduleDetailID">
            <Columns>
                <telerik:GridBoundColumn DataField="Type" HeaderText="Type"
                    UniqueName="Type" SortExpression="Type" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="WorkingHourName" HeaderText="Working Hour Name"
                    UniqueName="WorkingHourName" SortExpression="WorkingHourName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update Date Time"
                    UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="LastUpdateUserID" HeaderText="Last Update User ID"
                    UniqueName="LastUpdateUserID" SortExpression="LastUpdateUserID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
