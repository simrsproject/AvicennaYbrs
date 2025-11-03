<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="OperationalTimeList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.OperationalTimeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="OperationalTimeID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OperationalTimeID"
                    HeaderText="ID" UniqueName="OperationalTimeID" SortExpression="OperationalTimeID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OperationalTimeName" HeaderText="Operational Time Name"
                    UniqueName="OperationalTimeName" SortExpression="OperationalTimeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" DataField="OperationalTimeBackcolor"
                    HeaderText="Backcolor" UniqueName="OperationalTimeBackcolor" SortExpression="OperationalTimeBackcolor"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div style="height: 10px; width: 50px; background-color: <%#DataBinder.Eval(Container.DataItem,"OperationalTimeBackcolor")%>">
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Time1" HeaderText="Time 1"
                    UniqueName="Time1" SortExpression="Center" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Time2" HeaderText="Time 2"
                    UniqueName="Time2" SortExpression="Time2" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Time3" HeaderText="Time 3"
                    UniqueName="Time3" SortExpression="Time3" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Time4" HeaderText="Time 4"
                    UniqueName="Time4" SortExpression="Time4" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Time5" HeaderText="Time 5"
                    UniqueName="Time5" SortExpression="Time5" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
