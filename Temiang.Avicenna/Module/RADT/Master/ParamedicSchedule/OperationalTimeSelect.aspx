<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="OperationalTimeSelect.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.OperationalTimeSelect"
    Title="Schedule Setting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdOperationalTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOperationalTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="850">
        <tr>
            <td class="header" style="width: 200px">
                <asp:Label ID="Label1" runat="server" Text="1. Select Day or Date"></asp:Label>
            </td>
            <td class="header" style="width: 650px">
                <asp:Label ID="Label2" runat="server" Text="2. Select Operational Time"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 200px">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadCalendar ID="cldSchedule" runat="server" ShowOtherMonthsDays="False">
                            </telerik:RadCalendar>
                        </td>
                    </tr>
                    <tr runat="server" id="trRegType">
                        <td>
                            <asp:CheckBox runat="server" ID="chkIsIpr" Text="IPR" />&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ID="chkIsOpr" Text="OPR" />&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ID="chkIsEmr" Text="EMR" />
                        </td>
                    </tr>
                </table>

            </td>
            <td valign="top" style="width: 650px">
                <telerik:RadGrid ID="grdOperationalTime" runat="server" AutoGenerateColumns="false"
                    Width="650px" OnNeedDataSource="grdOperationalTime_NeedDataSource" AllowPaging="True"
                    PageSize="6">
                    <MasterTableView DataKeyNames="OperationalTimeID">
                        <Columns>
                            <telerik:GridTemplateColumn DataField="OperationalTimeID" HeaderText="ID" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"OperationalTimeBackcolor")%>">
                                        <%#DataBinder.Eval(Container.DataItem,"OperationalTimeID")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="OperationalTimeName" HeaderText="Operational Time"
                                UniqueName="OperationalTimeName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Time1" HeaderText="Time 1" UniqueName="Time1">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Time2" HeaderText="Time 2" UniqueName="Time2"
                                SortExpression="Time2">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Time3" HeaderText="Time 3" UniqueName="Time3">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Time4" HeaderText="Time 4" UniqueName="Time4">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Time5" HeaderText="Time 5" UniqueName="Time5">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
