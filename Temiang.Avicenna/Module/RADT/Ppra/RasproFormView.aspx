<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RasproFormView.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproFormView" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproFlowChart.ascx" TagPrefix="uc1" TagName="RasproFlowChart" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }
        </style>
        <script type="text/javascript">
        </script>
    </telerik:RadCodeBlock>

    <uc1:RasproHeader runat="server" ID="rasproHeader" />
    <div style="height: 4px;"></div>
    <div style="width: 40%; margin: auto; width: 600px; border: 3px solid #73AD21; padding: 10px;">
        <asp:Literal runat="server" ID="litRasproLine"></asp:Literal>
    </div>

    <fieldset>
            <asp:Literal runat="server" ID="litAntibioticSuggest"></asp:Literal>
        <telerik:RadGrid ID="grdLaboratoryCultureResult" runat="server" AutoGenerateColumns="False" GridLines="None">
            <MasterTableView DataKeyNames="OrderLabNo" GroupLoadMode="Client">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="TestGroup" HeaderText="Group" />
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="TestGroup" SortOrder="None" />
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridTemplateColumn DataField="LabOrderSummary" UniqueName="LabOrderSummary"
                        HeaderText="Exam Name" HeaderStyle-Width="250px">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn DataField="ResultDatetime" UniqueName="ResultDatetime" HeaderText="Result Date" HeaderStyle-Width="120px" />
                    <telerik:GridBoundColumn DataField="Flag" UniqueName="Flag" HeaderText="Flag" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Result")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                        HeaderStyle-Width="150px" />
                    <telerik:GridBoundColumn DataField="ResultComment" UniqueName="ResultComment" HeaderText="Result Comment" />
                    <telerik:GridBoundColumn DataField="LabOrderCode" UniqueName="LabOrderCode" HeaderText="Code" Display="False" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>

        <br />
        </fieldset>

</asp:Content>
