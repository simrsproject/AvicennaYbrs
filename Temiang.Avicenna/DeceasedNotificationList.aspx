<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeceasedNotificationList.aspx.cs" Inherits="Temiang.Avicenna.DeceasedNotificationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deceased Patient List</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="fw_RadScriptManager" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadSkinManager ID="fw_RadSkinManager" runat="server">
        </telerik:RadSkinManager>
        <div>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="RegistrationNo" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn DataField="DeceasedDateTime" HeaderText="Date" UniqueName="DeceasedDateTime"
                            SortExpression="DeceasedDateTime">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                            SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="140px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room Name" UniqueName="RoomName"
                            SortExpression="RoomName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed ID" UniqueName="BedID"
                            SortExpression="BedID">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </form>
</body>
</html>
