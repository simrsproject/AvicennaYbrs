<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeByServiceSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeByServiceSettingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnPreRender="grdList_PreRender">
        <MasterTableView DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn DataField="RegistrationTypeName" HeaderText="Registration Type" UniqueName="RegistrationTypeName"
                    SortExpression="RegistrationTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="300px" />
                <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                    SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeCaseTypeName" HeaderText="Paramedic Fee Case Type" UniqueName="ParamedicFeeCaseTypeName"
                    SortExpression="ParamedicFeeCaseTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeIsTeamName" HeaderText="Join Paramedic" UniqueName="ParamedicFeeIsTeamName"
                    SortExpression="ParamedicFeeIsTeamName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeTeamStatusName" HeaderText="Paramedic Fee Team Status" UniqueName="ParamedicFeeTeamStatusName"
                    SortExpression="ParamedicFeeTeamStatusName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tarif Component Name" UniqueName="TariffComponentName"
                    SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn DataField="IsFeeValueInPercent" HeaderText="Is Value In Percent" UniqueName="IsFeeValueInPercent"
                    SortExpression="IsFeeValueInPercent" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="FeeValue" HeaderText="Fee Value" UniqueName="FeeValue"
                    SortExpression="FeeValue" DataFormatString="{0:n2}" Visible="false">
                    <HeaderStyle HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="colFeeValue" HeaderText="Fee Value" HeaderStyle-Width="90">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtFeeValue" runat="server" Width="70" ReadOnly="true">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CountMax" HeaderText="Count Max" UniqueName="CountMax"
                    SortExpression="CountMax" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IgnoredIfAnyReplacement" HeaderText="Ignore If Any Replacement" UniqueName="IgnoredIfAnyReplacement"
                    SortExpression="IgnoredIfAnyReplacement">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsReplacement" HeaderText="Is Replacement" UniqueName="IsReplacement"
                    SortExpression="IsReplacement">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsReplacementForFeeByPercentageOfAR" HeaderText="Is Replacement For Fee By Percentage Of AR" UniqueName="IsReplacementForFeeByPercentageOfAR"
                    SortExpression="IsReplacementForFeeByPercentageOfAR">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
