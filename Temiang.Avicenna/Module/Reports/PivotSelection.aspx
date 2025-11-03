<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="PivotSelection.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.PivotSelection"
    Title="Untitled Page" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">

        <script type="text/javascript">
            function openRadWindow(id, pvtId) {
                var prgType = document.getElementById("hdnProgramType").value;
                var oWnd = radopen("ReportOption.aspx?id=" + id + "&pvtId=" + pvtId + "&tp=" + prgType, "RadWindow1");
                oWnd.center();
            }
            function onDeleteCustomPivot(id, pvtId, pvtName) {
                if (confirm('Delete custom Pivot ' + pvtName + ' ?')) {
                    <%--__doPostBack("<%= grdList.UniqueID %>", "rebind:" + id + ":" + pvtId);--%>
                    __doPostBack("<%= grdList.UniqueID %>", "rebind:" + id + ":" + pvtId + "|");
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindow1" Width="500px" Height="600px" runat="server"
        Style="z-index: 7001" Modal="true" VisibleStatusbar="false" DestroyOnClose="true"
        Behavior="Close" Behaviors="Maximize, Close, Move" ReloadOnShow="True" AutoSize="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchReport">
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
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search">
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="lblProgramName" runat="server" Text="Pivot Name" Width="100px"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtProgramName" runat="server" Width="300px" MaxLength="100" />
                    <asp:ImageButton ID="btnSearchReport" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnSearchReport_Click" />
                </td>
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" AllowSorting="true"
        ShowStatusBar="true" AllowPaging="true" PageSize="20" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ProgramID">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Visible="true">
                    <ItemTemplate>
                        <a href="#" onclick="openRadWindow('<%#DataBinder.Eval(Container.DataItem,"ProgramID")%>','<%#DataBinder.Eval(Container.DataItem,"CustomPivotID")%>'); return false;">Go</a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="process" HeaderText="" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Visible="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openRadWindow('{0}', '{1}'); return false;\"><img src=\"../../Images/go.png\" border=\"0\" alt=\"Go\" title=\"Go\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "ProgramID"), DataBinder.Eval(Container.DataItem, "CustomPivotID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="PivotName" HeaderText="Pivot Name"
                    UniqueName="PivotName" SortExpression="PivotName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" />
                <telerik:GridBoundColumn DataField="Note" HeaderText="Description" UniqueName="Note" HeaderStyle-Width="500px"
                    SortExpression="Note" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" />
                <telerik:GridTemplateColumn HeaderText="" Groupable="false" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "PivotType").Equals("STD") ? "" : string.Format("<a href=\"#\" onclick=\"onDeleteCustomPivot('{0}','{1}','{2}'); return false;\"><img src=\"../../Images/Toolbar/delete16.png\" border=\"0\" /></a>",DataBinder.Eval(Container.DataItem,"ProgramID"), DataBinder.Eval(Container.DataItem, "CustomPivotID"), DataBinder.Eval(Container.DataItem, "PivotName")))%>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
