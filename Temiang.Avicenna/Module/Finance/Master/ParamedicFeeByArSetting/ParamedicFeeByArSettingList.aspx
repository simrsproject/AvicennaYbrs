<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeByArSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeByArSettingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnPreRender="grdList_PreRender">
        <MasterTableView DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn DataField="RegistrationTypeName" HeaderText="Registration Type" UniqueName="RegistrationTypeName"
                    SortExpression="RegistrationTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="IsMergeToIPR" HeaderText="Merge To IPR" UniqueName="IsMergeToIPR"
                    SortExpression="IsMergeToIPR" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SmfName" HeaderText="SMF Name (Surgery Only)" UniqueName="SmfName"
                    SortExpression="SmfName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeCaseTypeName" HeaderText="Paramedic Fee Case Type" UniqueName="ParamedicFeeCaseTypeName"
                    SortExpression="ParamedicFeeCaseTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeIsTeamName" HeaderText="Join Paramedic" UniqueName="ParamedicFeeIsTeamName"
                    SortExpression="ParamedicFeeIsTeamName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="LosStart" HeaderText="Los Start" UniqueName="LosStart"
                    SortExpression="LosStart" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="40px" />
                <telerik:GridBoundColumn DataField="LosEnd" HeaderText="Los End" UniqueName="LosEnd"
                    SortExpression="LosEnd" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="50px" />
                <telerik:GridBoundColumn DataField="ParamedicFeeTeamStatusName" HeaderText="Paramedic Fee Team Status" UniqueName="ParamedicFeeTeamStatusName"
                    SortExpression="ParamedicFeeTeamStatusName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn DataField="IsFeeValueInPercent" HeaderText="Is Value In Percent" UniqueName="IsFeeValueInPercent"
                    SortExpression="IsFeeValueInPercent" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="FeeValue" HeaderText="Fee Value" UniqueName="FeeValue"
                    SortExpression="FeeValue" DataFormatString="{0:n2}" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="colFeeValue" HeaderText="Fee Value" HeaderStyle-Width="90">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtFeeValue" runat="server" Width="70" ReadOnly="true">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
