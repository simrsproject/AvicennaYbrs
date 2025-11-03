<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeRemunByIdiSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeRemunByIdiSettingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDataBound="grdList_DataBound"
        OnPreRender="grdList_PreRender" >
        <MasterTableView DataKeyNames="SettingID">
            <Columns>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SmfName" HeaderText="Smf" UniqueName="SmfName"
                    SortExpression="SmfName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
               
                <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group" UniqueName="ItemGroupName"
                    SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="MultiplierValue" HeaderText="Multiplier Value" UniqueName="MultiplierValue" 
                    SortExpression="MultiplierValue" DataFormatString="{0:n2}" >
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="unit" HeaderText="" UniqueName="unit"
                    SortExpression="unit">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Formula" HeaderText="Formula" UniqueName="Formula"
                    SortExpression="Formula" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
