<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="PatientSOAPEDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientSOAPEDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        
        function openWinDetail(recID, rtype) {          
            radopen("PatientSOAPEDetailItem.aspx?regno="+recID+"&rt="+rtype,"radWinDetail");
        }
                   
    </script>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close"
        ReloadOnShow="True" ShowContentDuringLoad="false" Title="SOAP Detail">
        <Windows>
            <telerik:RadWindow ID="radWinDetail" Width="970px" Height="400px" runat="server">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdRegisteredList" runat="server" OnNeedDataSource="grdRegisteredList_NeedDataSource"
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowSorting="True"
                    GridLines="None">
                    <MasterTableView DataKeyNames="RegistrationNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="view" Groupable="false">
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openWinDetail('{0}', '{1}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\"/></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRRegistrationType"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientID" HeaderText="Patient ID"
                                UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="RoomName" HeaderText="Division Name" UniqueName="RoomName"
                                SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID" SortExpression="BedID"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsClosed" HeaderText="Closed"
                                UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" AllowExpandCollapse="true">
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
