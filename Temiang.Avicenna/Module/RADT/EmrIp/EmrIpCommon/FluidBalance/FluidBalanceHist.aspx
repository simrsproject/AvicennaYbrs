<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="FluidBalanceHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.FluidBalanceHist" %>

<%@ Register Src="FluidBalanceCtl.ascx" TagPrefix="uc1" TagName="FluidBalanceCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var splitter = $find('<%= splMain.ClientID  %>');
                splitter.set_height(height - 30);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdHeader.ClientID %>");
                grid.get_element().style.height = height - 34 + "px";
                grid.repaint();

                grid = $find("<%= fluidBalanceCtl.GridFluidBalanceClientID %>");
                grid.get_element().style.height = height - 120 + "px";
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

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdHeader">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fluidBalanceCtl" />
                    <telerik:AjaxUpdatedControl ControlID="hdnEditRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="hdnEditSequenceNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:HiddenField runat="server" ID="hdnEditRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnEditSequenceNo" />

    <telerik:RadSplitter ID="splMain" runat="server" Width="100%" Height="590px" HeightOffset="30" Orientation="Vertical">
        <telerik:RadPane ID="paneLeft" runat="server" Width="250px">
            <telerik:RadGrid ID="grdHeader" runat="server" ShowStatusBar="true" OnNeedDataSource="grdHeader_NeedDataSource" OnSelectedIndexChanged="grdHeader_OnSelectedIndexChanged"
                AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <MasterTableView DataKeyNames="RegistrationNo,SequenceNo"
                    PageSize="24">
                    <Columns>
                        <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="No" UniqueName="SequenceNo"
                            SortExpression="SequenceNo">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="InOutDate" HeaderText="Date" UniqueName="InOutDate"
                            SortExpression="InOutDate">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="SchemaInfus" HeaderText="Schema Infus"
                            UniqueName="SchemaInfus" SortExpression="SchemaInfus">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                            SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3"></telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnablePostBackOnRowClick="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPane>

        <telerik:RadSplitBar ID="RadSplitbar1" runat="server" CollapseMode="Forward" />

        <telerik:RadPane ID="paneRight" runat="server">
            <uc1:FluidBalanceCtl runat="server" ID="fluidBalanceCtl" IsModeHistory="true" />
        </telerik:RadPane>
    </telerik:RadSplitter>

</asp:Content>
