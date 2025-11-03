<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NosocomialHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialHist" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdHeader">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="monitoringDetail" />
                    <telerik:AjaxUpdatedControl ControlID="hdnEditRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="hdnEditMonitoringNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdHeader" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:HiddenField runat="server" ID="hdnEditRegistrationNo"/>
    <asp:HiddenField runat="server" ID="hdnEditMonitoringNo"/>
    <telerik:RadSplitter ID="splMain" runat="server" Width="100%" Height="706px" HeightOffset="30" Orientation="Vertical">
        <telerik:RadPane ID="paneLeft" runat="server" Width="300px">
            <telerik:RadGrid ID="grdHeader" runat="server" ShowStatusBar="true" OnNeedDataSource="grdHeader_NeedDataSource" OnSelectedIndexChanged="grdHeader_OnSelectedIndexChanged"
                OnItemCommand="grdHeader_OnItemCommand" AutoGenerateColumns="False" AllowSorting="true" Height="700px">
                <MasterTableView DataKeyNames="RegistrationNo, MonitoringNo"
                    PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn DataField="MonitoringNo" HeaderText="No" UniqueName="MonitoringNo"
                            SortExpression="MonitoringNo" Display="False">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="InstallationDateTime" HeaderText="Date" UniqueName="InstallationDateTime"
                            SortExpression="InstallationDateTime">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="Location" UniqueName="Location" HeaderText="Location" HeaderStyle-Width="250px" />
                        <telerik:GridDateTimeColumn DataField="ReleaseDateTime" HeaderText="Release" UniqueName="ReleaseDateTime"
                            SortExpression="ReleaseDateTime">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                                                 SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center"/>
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnablePostBackOnRowClick="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPane>

        <telerik:RadSplitBar ID="splitBar" runat="server" CollapseMode="Forward" />

        <telerik:RadPane ID="paneRight" runat="server">
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var splitter = $find('<%= splMain.ClientID  %>');
                splitter.set_height(height - 40);

                grid = $find("<%= nosocomialCtl.GridClientID %>");
                grid.get_element().style.height = height - 160 + "px";
                grid.repaint();
            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
