<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeTrainingList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "EmployeeTrainingDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmployeeTrainingID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeTrainingID"
                    HeaderText="EmployeeTrainingID" UniqueName="EmployeeTrainingID" SortExpression="EmployeeTrainingID"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="EmployeeTrainingName" HeaderText="Training Name"
                    UniqueName="EmployeeTrainingName" SortExpression="EmployeeTrainingName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TrainingLocation" HeaderText="Training Location"
                    UniqueName="TrainingLocation" SortExpression="TrainingLocation" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TrainingOrganizer" HeaderText="Training Organizer"
                    UniqueName="TrainingOrganizer" SortExpression="TrainingOrganizer" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsInHouseTraining"
                    HeaderText="In-House" UniqueName="IsInHouseTraining" SortExpression="IsInHouseTraining"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsScheduledTraining" HeaderText="Scheduled"
                    UniqueName="IsScheduledTraining" SortExpression="IsScheduledTraining" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                    UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="SREmployeeTrainingDateSeparator" HeaderText=""
                    UniqueName="SREmployeeTrainingDateSeparator" SortExpression="SREmployeeTrainingDateSeparator" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                    UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="StartTime" HeaderText="Start Time"
                    UniqueName="StartTime" SortExpression="StartTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EndTime" HeaderText="End Time"
                    UniqueName="EndTime" SortExpression="EndTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="Attendance" HeaderText="Attendance"
                    UniqueName="Attendance" SortExpression="Attendance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
