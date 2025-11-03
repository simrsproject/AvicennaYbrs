<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RiskManagementList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.RiskManagementList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "PatientIncidentDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="PatientIncidentNo" ClientDataKeyNames="PatientIncidentNo">
            <Columns>
                <telerik:GridBoundColumn DataField="PatientIncidentNo" HeaderText="Incident No" UniqueName="PatientIncidentNo"
                    SortExpression="PatientIncidentNo">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IncidentDateTime" HeaderText="Incident Date"
                    UniqueName="IncidentDateTime" SortExpression="IncidentDateTime" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReportingDateTime" HeaderText="Reporting Date"
                    UniqueName="ReportingDateTime" SortExpression="ReportingDateTime" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IncidentGroupName" HeaderText="Incident Group"
                    UniqueName="IncidentGroupName" SortExpression="IncidentGroupName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>