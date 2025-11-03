<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ReportSelection.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportSelection"
    Title="Untitled Page" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function openRadWindow(id) {
            var prgType = document.getElementById("hdnProgramType").value;
            var oWnd = radopen("ReportOption.aspx?id=" + id + "&tp=" + prgType, "RadWindow1");
            oWnd.center();
        }
    </script>

    <telerik:RadWindowManager ID="RadWindow1" Width="500px" Height="600px" runat="server"
        Style="z-index: 7001" Modal="true" VisibleStatusbar="false" DestroyOnClose="true"
        Behavior="Close" Behaviors="Maximize, Close, Move" ReloadOnShow="True" AutoSize="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchModule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search">
        <table width="100%">
            <tr runat="server" id="trModule">
                <td class="label">
                    <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboModule" runat="server" Width="300px" 
                        HighlightTemplatedItems="true" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ModuleName")%>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:ImageButton ID="btnSearchModule" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnSearchReport_Click" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblProgramName" runat="server" Text="Report Name"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtProgramName" runat="server" Width="300px" MaxLength="100" />
                </td>
                <td>
                    <asp:ImageButton ID="btnSearchReport" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnSearchReport_Click" />
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" AllowSorting="true"
        ShowStatusBar="true" AllowPaging="true" PageSize="20" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ProgramID">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldAlias="ModuleName" FieldName="ModuleName" FormatString="{0}"
                            HeaderValueSeparator=" : " HeaderText="Module "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ModuleRowIndex" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <ItemTemplate>
                        <a href="#" onclick="openRadWindow('<%#DataBinder.Eval(Container.DataItem,"ProgramID")%>'); return false;"><i>Go</i></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="process" HeaderText="" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Visible="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openRadWindow('{0}'); return false;\"><img src=\"../../Images/go.png\" border=\"0\" title=\"Go\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "ProgramID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="ProgramName" HeaderText="Report Name"
                    UniqueName="ProgramName" SortExpression="ProgramName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" />
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Note" HeaderText="Description" UniqueName="Note"
                    SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"/>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ModuleName" HeaderText="Module"
                    UniqueName="ModuleName" SortExpression="ModuleName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"/>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
