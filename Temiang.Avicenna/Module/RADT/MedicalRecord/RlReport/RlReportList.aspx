<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="RlReportList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.RlReportList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(rptId, rptNo) {
                var url = '';
                switch (rptId) {
                    case "1":
                        url = 'RlReport1_2.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "2":
                        url = 'RlReport1_3.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "3":
                        url = 'RlReport2.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "4":
                        url = 'RlReport3_1.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "5":
                        url = 'RlReport3_2.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "6":
                        url = 'RlReport3_3.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "7":
                        url = 'RlReport3_4.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "8":
                        url = 'RlReport3_5.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "9":
                        url = 'RlReport3_6.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "10":
                        url = 'RlReport3_7.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "11":
                        url = 'RlReport3_8.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "12":
                        url = 'RlReport3_9.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "13":
                        url = 'RlReport3_10.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "14":
                        url = 'RlReport3_11.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "15":
                        url = 'RlReport3_12.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "16":
                        url = 'RlReport3_13.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "17":
                        url = 'RlReport3_13b.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "18":
                        url = 'RlReport3_14.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "19":
                        url = 'RlReport3_15.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "20":
                        url = 'RlReport4A.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "21":
                        url = 'RlReport4ASebab.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "22":
                        url = 'RlReport4B.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "23":
                        url = 'RlReport4BSebab.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "24":
                        url = 'RlReport5_1.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "25":
                        url = 'RlReport5_2.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "26":
                        url = 'RlReport5_3.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                    case "27":
                        url = 'RlReport5_4.aspx?md=view&rptId=' + rptId + '&rptNo=' + rptNo;
                        break;
                }
                window.location.href = url;
            }

            function gotoAddUrl(rptId) {
                var url = '';
                switch (rptId) {
                    case "1":
                        url = 'RlReport1_2.aspx?md=new&rptId=' + rptId;
                        break;
                    case "2":
                        url = 'RlReport1_3.aspx?md=new&rptId=' + rptId;
                        break;
                    case "3":
                        url = 'RlReport2.aspx?md=new&rptId=' + rptId;
                        break;
                    case "4":
                        url = 'RlReport3_1.aspx?md=new&rptId=' + rptId;
                        break;
                    case "5":
                        url = 'RlReport3_2.aspx?md=new&rptId=' + rptId;
                        break;
                    case "6":
                        url = 'RlReport3_3.aspx?md=new&rptId=' + rptId;
                        break;
                    case "7":
                        url = 'RlReport3_4.aspx?md=new&rptId=' + rptId;
                        break;
                    case "8":
                        url = 'RlReport3_5.aspx?md=new&rptId=' + rptId;
                        break;
                    case "9":
                        url = 'RlReport3_6.aspx?md=new&rptId=' + rptId;
                        break;
                    case "10":
                        url = 'RlReport3_7.aspx?md=new&rptId=' + rptId;
                        break;
                    case "11":
                        url = 'RlReport3_8.aspx?md=new&rptId=' + rptId;
                        break;
                    case "12":
                        url = 'RlReport3_9.aspx?md=new&rptId=' + rptId;
                        break;
                    case "13":
                        url = 'RlReport3_10.aspx?md=new&rptId=' + rptId;
                        break;
                    case "14":
                        url = 'RlReport3_11.aspx?md=new&rptId=' + rptId;
                        break;
                    case "15":
                        url = 'RlReport3_12.aspx?md=new&rptId=' + rptId;
                        break;
                    case "16":
                        url = 'RlReport3_13.aspx?md=new&rptId=' + rptId;
                        break;
                    case "17":
                        url = 'RlReport3_13b.aspx?md=new&rptId=' + rptId;
                        break;
                    case "18":
                        url = 'RlReport3_14.aspx?md=new&rptId=' + rptId;
                        break;
                    case "19":
                        url = 'RlReport3_15.aspx?md=new&rptId=' + rptId;
                        break;
                    case "20":
                        url = 'RlReport4A.aspx?md=new&rptId=' + rptId;
                        break;
                    case "21":
                        url = 'RlReport4ASebab.aspx?md=new&rptId=' + rptId;
                        break;
                    case "22":
                        url = 'RlReport4B.aspx?md=new&rptId=' + rptId;
                        break;
                    case "23":
                        url = 'RlReport4BSebab.aspx?md=new&rptId=' + rptId;
                        break;
                    case "24":
                        url = 'RlReport5_1.aspx?md=new&rptId=' + rptId;
                        break;
                    case "25":
                        url = 'RlReport5_2.aspx?md=new&rptId=' + rptId;
                        break;
                    case "26":
                        url = 'RlReport5_3.aspx?md=new&rptId=' + rptId;
                        break;
                    case "27":
                        url = 'RlReport5_4.aspx?md=new&rptId=' + rptId;
                        break;
                }
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" MaxLength="4" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterYear" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="RlMasterReportID">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserAddAble.Equals(false) ? string.Empty : 
                            string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                  DataBinder.Eval(Container.DataItem, "RlMasterReportID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RlMasterReportID"
                    HeaderText="RL Master Report ID" UniqueName="RlMasterReportID" SortExpression="RlMasterReportID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RlMasterReportNo" HeaderText="RL Master Report No"
                    UniqueName="RlMasterReportNo" SortExpression="RlMasterReportNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RlMasterReportName" HeaderText="RL Master Report Name"
                    UniqueName="RlMasterReportName" SortExpression="RlMasterReportName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="RlTxReportNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                                                              DataBinder.Eval(Container.DataItem, "RlMasterReportID"), DataBinder.Eval(Container.DataItem, "RlTxReportNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RlTxReportNo" HeaderText="Report No"
                            UniqueName="RlTxReportNo" SortExpression="RlTxReportNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="StartMonth" HeaderText="Start Month"
                            UniqueName="StartMonth" SortExpression="StartMonth" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="EndMonth" HeaderText="End Month"
                            UniqueName="EndMonth" SortExpression="EndMonth" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PeriodYear" HeaderText="Year"
                            UniqueName="PeriodYear" SortExpression="PeriodYear" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>    
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
