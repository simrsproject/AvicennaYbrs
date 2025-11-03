<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LaboratoryResultTest.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.MedicalHistory.LaboratoryResultTest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%" style="vertical-align: top">
                <telerik:RadGrid ID="grdLaboratory" runat="server" OnNeedDataSource="grdLaboratory_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" Height="770px"
                     OnItemDataBound="grdLaboratory_OnItemDataBound">
                    <MasterTableView DataKeyNames="TransactionNo,ResultValue" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridDateTimeColumn DataField="TransactionDate" UniqueName="TransactionDate"
                                HeaderText="Date" HeaderStyle-Width="80px" ItemStyle-VerticalAlign="Top" />
                            <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    Reg No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                    <br />
                                    Tx No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>
                                    <br />
                                    From:&nbsp;<b><%#DataBinder.Eval(Container.DataItem, "FromServiceUnitName")%></b>
                                    <br />
                                    By:&nbsp;<i> <%#DataBinder.Eval(Container.DataItem, "PhysicianSenders")%></i>
                                    <br />
                                    Order Item:
                            <br />
                                    <div style="padding-left: 10px;">
                                        <%#DataBinder.Eval(Container.DataItem, "JobOrderSummary")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="LaboratoryResult" HeaderText="Result">
                                <ItemTemplate>
                                    <%#LaboratoryResultNote(DataBinder.Eval(Container.DataItem, "TransactionNo").ToString())%>
                                    <telerik:RadGrid ID="grdLaboratoryResult" runat="server" AutoGenerateColumns="False" GridLines="None">
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
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="False">
                        <Selecting AllowRowSelect="False" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
